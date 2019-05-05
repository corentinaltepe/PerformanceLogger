using PerformanceLogger.Targets;

namespace PerformanceLogger.Extensions.Postgres
{
    /// <summary>
    /// Postgres database as target for logging the performance of the application tracked.
    /// </summary>
    class PostgresTarget : ITarget
    {
        private readonly BatchWriter _writer;
        public PostgresTarget(BatchWriter writer)
        {
            _writer = writer;
        }

        public void Log(PerformanceResult report)
        {
            // The writer adds the reports to the database by batches in parallel
            _writer.Write(report);
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
