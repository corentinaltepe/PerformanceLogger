using System.Collections.Generic;
using System.Linq;
using PerformanceLogger.Targets;

namespace PerformanceLogger
{
    /// <summary>
    /// Configures and builds a PerformanceLogger
    /// </summary>
    public class PerformanceLoggerBuilder
    {
        private readonly List<ITarget> _targets = new List<ITarget>();
        public PerformanceLoggerBuilder AddTarget(ITarget target)
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