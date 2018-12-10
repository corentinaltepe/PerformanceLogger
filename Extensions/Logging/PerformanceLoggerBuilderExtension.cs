﻿using System;
using Microsoft.Extensions.Logging;

namespace PerformanceLogger.Extensions.Logging
{
    // Extension methods must be defined in a static class.
    public static class PerformanceLoggerBuilderExtension
    {
        /// <summary>
        /// Adds an ILogger as a target to logging the performance counts
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static PerformanceLoggerBuilder AddLogger(this PerformanceLoggerBuilder builder, ILogger logger)
        {
            // Create target from the logger
            var loggingTarget = new LoggerTarget(logger);
            return builder.AddTarget(loggingTarget);
        }
    }
}