using System;
using System.Diagnostics;
using PerformanceLogger.Targets;

namespace PerformanceLogger
{
    class PerformanceLogger : IPerformanceLogger
    {
        private readonly IClock _clock;
        private readonly ITarget _target;
        public PerformanceLogger(
            IClock clock,
            ITarget target)
        {
            _clock = clock;
            _target = target;
        }

        public IPerformanceLog Start()
        {
            return Start(null);
        }

        public IPerformanceLog Start(string eventId)
        {
            // Start tracking time
            var stopwatch = Stopwatch.StartNew();

            // Initiate what can be of the performance report
            var report = new PerformanceResult {
                EventId = eventId,
                StartDate = _clock.Now
            };

            return new PerformanceLog(stopwatch, report, _target);
        }
        
        /// <summary>
        /// Disposes the ITarget
        /// </summary>
        public void Dispose()
        {
            _target.Dispose();
        }
    }
}