namespace PerformanceLogger.Targets
{
    /// <summary>
    /// Logging target, which writes the results of a IPerformanceLog
    /// </summary>
    public interface ITarget
    {
        /// <summary>
        /// Logs the report of a single performance analysis
        /// </summary>
        void Log(PerformanceResult report);
    }
}