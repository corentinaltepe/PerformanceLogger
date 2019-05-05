using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace PerformanceLogger.Extensions.Postgres
{
    /// <summary>
    /// Writes logs to a Postgres database by batches read from the queue of logs
    /// </summary>
    class BatchWriter : IDisposable
    {
        /// <summary>
        /// Task to be executed in a separate thread for writing the logs to
        /// the database by batches.
        /// </summary>
        /// <returns></returns>
        private Task BatchWriting;

        private readonly string _connectionString;
        private readonly string _tableName;
        private readonly ILogger<BatchWriter> _logger;
        private readonly ConcurrentQueue<PerformanceResult> _logs = new ConcurrentQueue<PerformanceResult>();
        
        public BatchWriter(string connectionString, string tableName, ILogger<BatchWriter> logger)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _logger = logger;
        }
        
        private bool _tableCreationChecked = false;
        /// <summary>
        /// Creates the table if it doesn't exist
        /// </summary>
        private void CreateTableIfNotExists()
        {
            // Execute this method only once, at first call
            if(_tableCreationChecked)
                return;

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $@"
CREATE TABLE IF NOT EXISTS {_tableName} (
    id		    SERIAL,
    eventId		text,
    date 		timestamp,
    durationMs	double precision,
    ticks       bigint
);";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private readonly object writeLock = new object();
        /// <summary>
        /// Adds a log to be written to the database. Can be called concurrently.
        /// </summary>
        /// <param name="log"></param>
        public void Write(PerformanceResult log)
        {
            // Add the log to the queue
            _logs.Enqueue(log);

            // Start the batch writing process if not started already
            lock(writeLock) {
                if(BatchWriting is null || BatchWriting.IsCompleted)
                {
                    // Initialize the task to be executed in parallel
                    BatchWriting = new Task(WriteASingleBatch);
                    BatchWriting.Start();
                }
            }
        }

        private void WriteASingleBatch()
        {
            try {
                CreateTableIfNotExists();
                WriteASingleBatchWithoutTryCatch();
            } catch(Exception ex)
            {
                _logger.LogWarning(ex, $"Failed to batch write the logs to the Postgres database.", _connectionString, _tableName);
            }
        }

        /// <summary>
        /// Builds a COPY statement and sends it to the database for a single batch write
        /// </summary>
        private void WriteASingleBatchWithoutTryCatch()
        {
            // No log to write, return without opening a connection to the database
            if(_logs.IsEmpty)
                return;

            // Establish a connection
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                // Note : the BINARY copy seems to not work on Postgres 9.2. Not tested with other versions.
                using (var writer = conn.BeginTextImport($"COPY {_tableName} (eventid, date, durationms, ticks) FROM STDIN"))
                {
                    // Read each log in the queue and write it in the SQL statement
                    while(_logs.TryDequeue(out var report)) {
                        writer.Write($"{report.EventId}\t{report.StartDate.ToUniversalTime().ToString("o")}\t{report.Duration.TotalMilliseconds}\t{report.Ticks}\n");
                    }
                }
            }
        }

        /// <summary>
        /// Waits synchronously for the task to terminate before disposing
        /// </summary>
        public void Dispose()
        {
            if(BatchWriting != null && !BatchWriting.IsCompleted && BatchWriting.Status != TaskStatus.Created)
                BatchWriting.Wait();
        }
    }
}