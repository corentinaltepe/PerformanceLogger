using System.Diagnostics;
using PerformanceLogger.ITargets;

namespace PerformanceLogger
{
    public class PerformanceLog : IPerformanceLog
    {
        private readonly Stopwatch _stopwatch;
        private readonly PerformanceResult _report;
        private readonly ITarget _target;
        public PerformanceLog(
            Stopwatch stopwatch,
            PerformanceResult report,
            ITarget target)
        {
            _stopwatch = stopwatch;
            _report = report;
            _target = target;
        }

        /// <summary>
        /// Ends the trackings of performance and logs it to all targets
        /// </summary>
        public void End()
        {
            // Stop and record stopwatch
            _stopwatch.Stop();
            _report.Duration = _stopwatch.Elapsed;

            // Write the result to the target(s)
            _target.Log(_report);
        }
    }
}