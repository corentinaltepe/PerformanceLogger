using System;
using System.Diagnostics;

namespace perflogger
{
    public class PerformanceLogger : IPerformanceLogger
    {
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
                StartDate = DateTime.Now
            };

            return new PerformanceLog(stopwatch, report);
        }
    }
}