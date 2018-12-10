using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PerformanceLogger.Targets;

namespace PerformanceLogger
{
    /// <summary>
    /// Configures and builds a PerformanceLogger
    /// </summary>
    public class PerformanceLoggerBuilder
    {
        private readonly List<ILogger> _loggers = new List<ILogger>();
        /// <summary>
        /// Adds a logger implementing Microsoft.Extensions.Logging.ILogger
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public PerformanceLoggerBuilder AddLogging(ILogger logger)
        {
            _loggers.Add(logger);
            return this;
        }

        public IPerformanceLogger Build()
        {
            // Create targets from the loggers
            var loggingTargets = _loggers.Select(logger => new LoggerTarget(logger));

            // Aggregate the targets
            var loggingTarget = new TargetAggregate(loggingTargets);

            // Instanciate the PerformanceLogger
            return new PerformanceLogger(new Clock(), loggingTarget);
        }
    }
}