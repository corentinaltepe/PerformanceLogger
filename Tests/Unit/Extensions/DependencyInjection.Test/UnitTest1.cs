using System;
using Xunit;
using PerformanceLogger.Extensions.DependencyInjection.AutoDecorators;

namespace PerformanceLogger.Extensions.DependencyInjection.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var serviceA = new ServiceA();
            var decorator = new PerformanceLoggingDecorator<ServiceA>();
            var perfServiceA = decorator.Decorate(serviceA);
        }

        public class ServiceA
        {

        }
    }
}
