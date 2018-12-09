using System;
using System.Diagnostics;

namespace perflogger
{
    public class PerformanceLogger : IPerformanceLogger
    {
        private readonly IClock _clock;
        public PerformanceLogger(IClock clock)
        {
            _clock = clock;
        }

        public IPerformanceLog Start()
        {
            return Start(null);
        }

        public IPerformanceLog Start(string eventId)
        {
            // Start tracking time
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Initiate what can be of the performance report
            var report = new PerformanceResult {
                EventId = eventId,
                StartDate = _clock.Now
            };

            return new PerformanceLog(stopwatch, report);
        }
    }
}