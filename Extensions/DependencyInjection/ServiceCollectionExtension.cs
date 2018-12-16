using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PerformanceLogger.Targets;

namespace PerformanceLogger.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds PerformanceLogger to the collection of services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddPerformanceLogger(
            this IServiceCollection services,
            Action<PerformanceLoggerDiOptions> configure)
        {
            services.AddTransient<IPerformanceLogger>(provider => {
                var builder = new PerformanceLoggerBuilder(provider.GetService<ILoggerFactory>());

                // Get the list of ITarget services and add them to the PerformanceLoggerBuilder
                var targets = provider.GetServices<ITarget>();
                foreach(var target in targets)
                    builder.AddTarget(target);

                return builder.Build();
            });

            configure(new PerformanceLoggerDiOptions(services));

            return services;
        }
    }
}