using System;

namespace PerformanceLogger
{
    /// <summary>
    /// Performance Logger
    /// </summary>
    public interface IPerformanceLogger : IDisposable
    {
        /// <summary>
        /// Initiates the tracking of a unit of work's performance
        /// and returns the IPerformanceLog, to be used to end the tracking.
        /// </summary>
        /// <returns></returns>
        IPerformanceLog Start();

        /// <summary>
        /// Initiates the tracking of a unit of work's performance
        /// and returns the IPerformanceLog, to be used to end the tracking.
        /// </summary>
        /// <param name="eventId">A unique ID used to identify the event, method or unit of work</param>
        /// <returns></returns>
        IPerformanceLog Start(string eventId);
    }
}
