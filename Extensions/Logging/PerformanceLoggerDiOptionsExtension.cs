using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PerformanceLogger.Extensions.DependencyInjection;
using PerformanceLogger.Targets;

namespace PerformanceLogger.Extensions.Logging
{
    public static class PerformanceLoggerDiOptionsExtension
    {
        /// <summary>
        /// Adds an ILogger as a target to logging the performance counts
        /// </summary>
        /// <returns></returns>
        public static void AddLogging(this PerformanceLoggerDiOptions options)
        {
            options.Services.Add(ServiceDescriptor.Transient<ITarget, LoggerTarget>());
        }

        /// <summary>
        /// Adds an ILogger as a target to logging the performance counts
        /// </summary>
        /// <returns></returns>
        public static void AddLogging(this PerformanceLoggerDiOptions options, Func<IServiceProvider, ILogger> loggerProvider)
        {
            options.Services.Add(ServiceDescriptor.Transient<ITarget>(provider => new LoggerTarget(loggerProvider(provider))));
        }
    }
}