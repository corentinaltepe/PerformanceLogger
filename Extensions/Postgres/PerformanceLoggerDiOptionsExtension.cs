using PerformanceLogger.Extensions.DependencyInjection;
using PerformanceLogger.Targets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PerformanceLogger.Extensions.Postgres
{
    public static class PerformanceLoggerDiOptionsExtension
    {
        /// <summary>
        /// Adds an PostgresTarget as a target to logging the performance counts
        /// </summary>
        /// <returns></returns>
        public static void AddPostgres(this PerformanceLoggerDiOptions options, 
            string connectionString,
            string tableName = "perflogs")
        {
            // Register the batcher as a singleton
            options.Services.AddSingleton<BatchWriter>(provider => new BatchWriter(
                connectionString, 
                tableName,
                provider.GetService<ILogger<BatchWriter>>()));

            options.Services.Add(ServiceDescriptor.Transient<ITarget, PostgresTarget>());
        }
    }
}