using Xunit;
using Microsoft.Extensions.Logging.Abstractions;
using PerformanceLogger.Extensions.AutoLogger;
using PerformanceLogger.Extensions.DependencyInjection.Test.Services;

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

            // Call the public methods
            perfServiceA.VoidMethodNoArgument();
            perfServiceA.VoidMethodOneArgument(10);
        }

        [Fact]
        public void DecorateInterfaceWithInternalImplementingClass()
        {
            var serviceB = new ServiceB();
            var decorator = new PerformanceLoggingDecoratorFactory(_logger);
            var perfServiceB = decorator.Decorate<IService>(serviceB);
            Assert.IsAssignableFrom<IService>(perfServiceB);

            // Call the public methods
            perfServiceB.VoidMethodNoArgument();
            perfServiceB.VoidMethodOneArgument(10);
        }

        [Fact]
        public void DecorateInterfaceWithPublicImplementingClass()
        {
            var serviceA = new ServiceA();
            var decorator = new PerformanceLoggingDecoratorFactory(_logger);
            var perfServiceA = decorator.Decorate<IService>(serviceA);
            Assert.IsAssignableFrom<IService>(perfServiceA);
            
            // Call the public methods
            perfServiceA.VoidMethodNoArgument();
            perfServiceA.VoidMethodOneArgument(10);
        }
    }
}
