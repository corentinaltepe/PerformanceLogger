using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace PerformanceLogger.Extensions.DependencyInjection.AutoDecorators
{
    /// <summary>
    /// Decorates any call to method or property with performance logging
    /// </summary>
    class PerformanceLoggerSelector : IInterceptor
    {
        private readonly IPerformanceLogger _logger;
        public PerformanceLoggerSelector(IPerformanceLogger logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            // Start logging
            var track = _logger.Start(invocation.Method.Name);

            // Execute
            invocation.Proceed();

            // End performance tracking
            track.End();
        }
    }
}