using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using PerformanceLogger.Targets;

[assembly: InternalsVisibleTo("PerformanceLogger.Test")]
namespace PerformanceLogger
{
    /// <summary>
    /// Configures and builds a PerformanceLogger
    /// </summary>
    public class PerformanceLoggerBuilder : IPerformanceLoggerBuilder
    {
        private readonly List<ITarget> _targets = new List<ITarget>();

        public ILoggerFactory LoggerFactory { get; }

        public PerformanceLoggerBuilder(ILoggerFactory loggerFactory) 
        {
            LoggerFactory = loggerFactory;
        }
        
        public IPerformanceLoggerBuilder AddTarget(ITarget target)
        {
            _targets.Add(target);
            return this;
        }

        public IPerformanceLogger Build()
        {
            // Aggregate the targets
            var loggingTarget = new TargetAggregate(_targets);

            // Instanciate the PerformanceLogger
            return new PerformanceLogger(new Clock(), loggingTarget);
        }
    }
}