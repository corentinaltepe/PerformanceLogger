using System;

namespace PerformanceLogger
{
    /// <summary>
    /// Tracks the performance (duration) of a single unit of work
    /// </summary>
    public interface IPerformanceLog : IDisposable
    {
        /// <summary>
        /// Ends the tracking of a single unit of work's performance
        /// </summary>
        void End();
    }
}