using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PerformanceLogger.Extensions.AutoLogger;
using PerformanceLogger.Targets;

[assembly: InternalsVisibleTo("PerformanceLogger.Extensions.DependencyInjection.Test")]
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
        /// Holds a reference to the one and only PerformanceLoggingDecoratorFactory to avoid
        /// memory leaks and keep performance good.
        /// </summary>
        private static PerformanceLoggingDecoratorFactory DecoratorFactory;

        /// <summary>
        /// Instanciates a new, single PerformanceLoggingDecoratorFactory if not done already, 
        /// and then returns the singleton.
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private static PerformanceLoggingDecoratorFactory GetDecoratorFactory(IServiceProvider provider) {
            if(DecoratorFactory == null)
                DecoratorFactory = new PerformanceLoggingDecoratorFactory(provider.GetService<IPerformanceLogger>());
            return DecoratorFactory;
        }

        /// <summary>
        /// Adds PerformanceLogger to the interface or class T to be decorated
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoPerformanceLogging<T>(
            this IServiceCollection services) where T : class
        {
            // Decorate the service with a performance logger decorator
            services.Decorate<T>((inner, provider) => { 
                var decoratorFactory = GetDecoratorFactory(provider);
                return decoratorFactory.Decorate<T>(inner);
            });
            return services;
        }
    }
}