using System;
using Microsoft.Extensions.DependencyInjection;

namespace PerformanceLogger.Extensions.DependencyInjection
{
    public class PerformanceLoggerDiOptions
    {
        public IServiceCollection Services { get; }

        public PerformanceLoggerDiOptions(IServiceCollection services)
        {
            Services = services;
        }
    }
}