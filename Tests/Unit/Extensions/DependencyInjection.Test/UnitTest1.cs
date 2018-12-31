using System;
using Xunit;
using PerformanceLogger.Extensions.DependencyInjection.AutoDecorators;
using PerformanceLogger.Extensions.DependencyInjection.Test.Services;

namespace PerformanceLogger.Extensions.DependencyInjection.Test
{
    public class UnitTest1
    {
        [Fact]
        public void DecoratePublicClass()
        {
            var serviceA = new ServiceA();
            var decorator = new PerformanceLoggingDecorator();
            var perfServiceA = decorator.Decorate(serviceA);
            Assert.IsAssignableFrom<ServiceA>(perfServiceA);
        }

        [Fact]
        public void DecorateInterfaceWithInternalImplementingClass()
        {
            var serviceB = new ServiceB();
            var decorator = new PerformanceLoggingDecorator();
            var perfServiceB = decorator.Decorate<IService>(serviceB);
            Assert.IsAssignableFrom<IService>(perfServiceB);
        }

        [Fact]
        public void DecorateInterfaceWithPublicImplementingClass()
        {
            var serviceA = new ServiceA();
            var decorator = new PerformanceLoggingDecorator();
            var perfServiceA = decorator.Decorate<IService>(serviceA);
            Assert.IsAssignableFrom<IService>(perfServiceA);
        }
    }
}
