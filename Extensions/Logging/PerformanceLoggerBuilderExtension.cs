using System;

namespace PerformanceLogger.Extensions.Logging
{
    // Extension methods must be defined in a static class.
    public static class PerformanceLoggerBuilderExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.
        public static int WordCount(this PerformanceLoggerBuilder builder)
        {
            // Create targets from the loggers
            var loggingTargets = _loggers.Select(logger => new LoggerTarget(logger));
            return 0;
        }
    }
}
