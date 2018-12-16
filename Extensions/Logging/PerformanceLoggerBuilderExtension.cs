using System;
using Microsoft.Extensions.Logging;

namespace PerformanceLogger.Extensions.Logging
{
    public static class PerformanceLoggerBuilderExtension
    {
        /// <summary>
        /// Adds an ILogger as a target to logging the performance counts
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static IPerformanceLoggerBuilder AddLogger(this IPerformanceLoggerBuilder builder, ILogger logger)
        {
            // Create target from the logger
            var loggingTarget = new LoggerTarget(logger);
            return builder.AddTarget(loggingTarget);
        }
    }
}
