using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PerformanceLogger.Targets;

[assembly: InternalsVisibleTo("PerformanceLogger.Extensions.DependencyInjection.Test")]

// TODO : remove this one
[assembly: InternalsVisibleTo("Sample")]

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

        /// <summary>
        /// Adds PerformanceLogger to the collection of services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoPerformanceLogging(
            this IServiceCollection services,
            IEnumerable<Type> serviceTypes)
        {
            // TODO: Auto-decorate on demand the services that need performance monitoring
            return services;
        }
    }
}