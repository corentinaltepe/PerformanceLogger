using System;
using Microsoft.Extensions.Logging;

namespace PerformanceLogger.Extensions.Postgres
{
    public static class PerformanceLoggerBuilderExtension
    {
        /// <summary>
        /// Adds a Postgres database as a target for logging the performance of the application.
        /// The table will be created in the database if it does not exist. The default table
        /// name will be perflogs.
        /// </summary>
        public static IPerformanceLoggerBuilder AddPostgres(
            this IPerformanceLoggerBuilder builder, 
            string connectionString,
            string tableName = "perflogs")
        {
            // Create target and its batch writer
            var batchWriter = new BatchWriter(connectionString, tableName, builder.LoggerFactory.CreateLogger<BatchWriter>());
            var loggingTarget = new PostgresTarget(batchWriter);
            return builder.AddTarget(loggingTarget);
        }
    }
}
