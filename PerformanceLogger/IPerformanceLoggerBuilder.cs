using Microsoft.Extensions.Logging;
using PerformanceLogger.Targets;

namespace PerformanceLogger
{
    public interface IPerformanceLoggerBuilder
    {
        /// <summary>
        /// Creates ILoggers
        /// </summary>
        /// <value></value>
        ILoggerFactory LoggerFactory { get; }
        
        IPerformanceLoggerBuilder AddTarget(ITarget target);
        IPerformanceLogger Build();
    }
}