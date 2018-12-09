namespace PerformanceLogger
{
    /// <summary>
    /// Tracks the performance (duration) of a single unit of work
    /// </summary>
    public interface IPerformanceLog
    {
        /// <summary>
        /// Ends the tracking of a single unit of work's performance
        /// </summary>
        void End();
    }
}