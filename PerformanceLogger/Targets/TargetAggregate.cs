using System.Collections.Generic;
using System.Linq;

namespace PerformanceLogger.Targets
{
    /// <summary>
    /// Aggregates several ITarget under one interface
    /// </summary>
    class TargetAggregate : ITarget
    {
        private readonly List<ITarget> _targets;
        public TargetAggregate(IEnumerable<ITarget> targets)
        {
            _targets = targets.ToList();
        }

        public void Log(PerformanceResult report)
        {
            _targets.ForEach(t => t.Log(report));
        }

        public void Dispose()
        {
            _targets.ForEach(t => t.Dispose());
        }
    }
}