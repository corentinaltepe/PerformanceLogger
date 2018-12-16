using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace PerformanceLogger.Targets
{
    /// <summary>
    /// Aggregates several ITarget under one interface
    /// </summary>
    class TargetAggregate : ITarget
    {
        private readonly ILogger<TargetAggregate> _logger;

        public TargetAggregate() { }
        public TargetAggregate(ILogger<TargetAggregate> logger)
        {
            _logger = logger;
        }

        private readonly List<ITarget> _targets;
        public TargetAggregate(IEnumerable<ITarget> targets)
        {
            _targets = targets.ToList();
        }

        public void Log(PerformanceResult report)
        {
            _targets.ForEach(target => {
                try 
                {
                    target.Log(report);
                } 
                catch(Exception ex) 
                {
                    if(_logger != null)
                        _logger.LogWarning(ex, $"Failed to log the report on {report.EventId} to the target {target.GetType()}.");
                }
            });
        }

        public void Dispose()
        {
            _targets.ForEach(t => t.Dispose());
        }
    }
}