using System.Diagnostics;

namespace perflogger
{
    public class PerformanceLog : IPerformanceLog
    {
        private readonly Stopwatch _stopwatch;
        private readonly PerformanceResult _report;
        public PerformanceLog(
            Stopwatch stopwatch,
            PerformanceResult report)
        {
            _stopwatch = stopwatch;
            _report = report;
        }

        /// <summary>
        /// Ends the trackings of performance and logs it to all targets
        /// </summary>
        public void End()
        {
            throw new System.NotImplementedException();
        }
    }
}