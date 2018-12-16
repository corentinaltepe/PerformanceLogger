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

        public void AutoLogPerformance(Type serviceType)
        {
            Console.WriteLine($"Configuring decorator for {serviceType.Name}");
            // WIP
            // TODO : read https://github.com/castleproject/Core/blob/master/docs/dynamicproxy-introduction.md
        }
    }
}