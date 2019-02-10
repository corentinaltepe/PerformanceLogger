using System;

namespace PerformanceLogger
{
    /// <summary>
    /// Results of a single performance analysis, for a unit of work, method execution
    /// or event processing.
    /// </summary>
    public class PerformanceResult
    {
        public string EventId { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public long Ticks { get;set; }
    }
}