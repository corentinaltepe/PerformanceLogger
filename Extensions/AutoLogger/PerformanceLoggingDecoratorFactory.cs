using System.Runtime.CompilerServices;
using Castle.DynamicProxy;

[assembly: InternalsVisibleTo("PerformanceLogger.Extensions.AutoLogger.Test")]
namespace PerformanceLogger.Extensions.AutoLogger
{
    /// <summary>
    /// Decorates any interface or class T with performance logging
    /// </summary>
    public class PerformanceLoggingDecoratorFactory
    {
        private readonly IPerformanceLogger _logger;
        public PerformanceLoggingDecoratorFactory(IPerformanceLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns the service decorated with performance logging on all public
        /// methods and properties
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public T Decorate<T>(T service) where T : class
        {
            var generator = new ProxyGenerator();
            var loggerSelector = new PerformanceLoggerSelector(_logger);
            var decoratorSelector = new DecoratorSelector(loggerSelector);
            var options = new ProxyGenerationOptions
            {
                Selector = decoratorSelector
            };

            if(typeof(T).IsInterface)
                return (T)generator.CreateInterfaceProxyWithTarget(typeof(T), service, options);
            return (T)generator.CreateClassProxyWithTarget(typeof(T), service, options);
        }
    }
}