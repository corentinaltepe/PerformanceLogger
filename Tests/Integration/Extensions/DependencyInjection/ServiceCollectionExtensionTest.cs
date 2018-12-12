using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using DependencyInjection.SampleServices;

namespace PerformanceLogger.Extensions.DependencyInjection
{
    public class ServiceCollectionExtensionTest
    {
        [Fact]
        public void AddPerformanceLogger()
        {
            // Configure the dependency injection framework with a performance logger
            var provider = new ServiceCollection()
                .AddPerformanceLogger(options => {
                    
                })
                .AddTransient<IService, ServiceA>()
                .BuildServiceProvider();

            // Resolve the registered IService
            var service = provider.GetService<IService>();

            // Verify the resolved service
            Assert.IsType<ServiceA>(service);
            Assert.IsAssignableFrom<IService>(service);
        }
    }
}
