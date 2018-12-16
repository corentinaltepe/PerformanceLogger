using System;
using PerformanceLogger.Targets;
using Microsoft.Extensions.Logging;

namespace PerformanceLogger.Extensions.Logging
{
    /// <summary>
    /// Adapts ILogger interface to ITarget to write performance logs to an ILogger
    /// </summary>
    class LoggerTarget : ITarget
    {
        private readonly ILogger _logger;
        public LoggerTarget(ILogger logger)
        {
            _logger = logger;
        }
        public LoggerTarget(ILogger<LoggerTarget> logger)
        {
            _logger = logger;
        }

        public void Log(PerformanceResult report)
        {
            // Format the entry and log it
            _logger.LogInformation($"{report.StartDate.ToShortDateString()} {report.StartDate.ToShortTimeString()};{report.EventId};{report.Duration.TotalMilliseconds} ms;");
        }
        
        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}