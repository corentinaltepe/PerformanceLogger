using System;
using Xunit;
using PerformanceLogger.Extensions.DependencyInjection.AutoDecorators;
using PerformanceLogger.Extensions.DependencyInjection.Test.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace PerformanceLogger.Extensions.DependencyInjection.Test
{
    public class PerformanceLoggingDecoratorTest
    {
        private readonly IPerformanceLogger _logger;

        public PerformanceLoggingDecoratorTest()
        {
            _logger = new PerformanceLoggerBuilder(new NullLoggerFactory()).Build();
        }

        [Fact]
        public void DecoratePublicClass()
        {
            var serviceA = new ServiceA();
            var decorator = new PerformanceLoggingDecoratorFactory(_logger);
            var perfServiceA = decorator.Decorate(serviceA);
            Assert.IsAssignableFrom<ServiceA>(perfServiceA);
        }

        [Fact]
        public void DecorateInterfaceWithInternalImplementingClass()
        {
            var serviceB = new ServiceB();
            var decorator = new PerformanceLoggingDecoratorFactory(_logger);
            var perfServiceB = decorator.Decorate<IService>(serviceB);
            Assert.IsAssignableFrom<IService>(perfServiceB);
        }

        [Fact]
        public void DecorateInterfaceWithPublicImplementingClass()
        {
            var serviceA = new ServiceA();
            var decorator = new PerformanceLoggingDecoratorFactory(_logger);
            var perfServiceA = decorator.Decorate<IService>(serviceA);
            Assert.IsAssignableFrom<IService>(perfServiceA);
        }
    }
}
