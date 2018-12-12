using PerformanceLogger;
using PerformanceLogger.Targets;

namespace PerformanceLogger
{
    public interface IPerformanceLoggerBuilder
    {
        IPerformanceLoggerBuilder AddTarget(ITarget target);
        IPerformanceLogger Build();
    }
}