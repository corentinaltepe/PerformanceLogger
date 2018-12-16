using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using DependencyInjection.SampleServices;
using PerformanceLogger.Extensions.Logging;
using PerformanceLogger.Extensions.Postgres;
using PerformanceLogger.Extensions.DependencyInjection;
using Moq;
using Microsoft.Extensions.Logging;

namespace PerformanceLogger.Extensions.DependencyInjection.Test
{
    public class ServiceCollectionExtensionTest
    {
        [Fact]
        public void AddPerformanceLogger()
        {
            // Configure the dependency injection framework with a performance logger
            var provider = new ServiceCollection()
                .AddLogging()
                .AddPerformanceLogger(options => {
                    options.AddLogging();
                    options.AddLogging(p => p.GetService<ILogger<ServiceCollectionExtensionTest>>());
                    options.AddPostgres("Host=localhost;Username=postgres;Password=MYPASSWORD;Database=performancelogs", "logs");
                })
                .AddTransient<IService, ServiceA>()
                .BuildServiceProvider();
            
            // Resolve the registered IService
            var service = provider.GetService<IService>();
            service.ExecuteSomething(); // Force performance logging

            // Verify the resolved service
            Assert.IsType<ServiceA>(service);
            Assert.IsAssignableFrom<IService>(service);

            // Resolve a PerformanceLogger
            var performanceLogger = provider.GetService<IPerformanceLogger>();
            Assert.NotNull(performanceLogger);

            // Flush the loggers before exiting
            provider.Dispose();
        }
    }
}
