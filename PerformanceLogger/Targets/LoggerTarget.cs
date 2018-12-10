using System;
using Microsoft.Extensions.Logging;

namespace PerformanceLogger.Targets
{
    /// <summary>
    /// Adapts ILogger interface to ITarget to write performance logs to an ILogger
    /// </summary>
    public class LoggerTarget : ITarget
    {
        private readonly ILogger _logger;
        public LoggerTarget(ILogger logger)
        {
            _logger = logger;
        }

        public void Log(PerformanceResult report)
        {
            // Format the entry and log it
            _logger.LogInformation($"{report.StartDate.ToShortDateString()} {report.StartDate.ToShortTimeString()};{report.EventId};{report.Duration.TotalMilliseconds} ms;");
        }
    }
}