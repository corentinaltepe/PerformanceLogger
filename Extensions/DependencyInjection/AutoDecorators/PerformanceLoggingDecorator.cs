using Castle.DynamicProxy;

namespace PerformanceLogger.Extensions.DependencyInjection.AutoDecorators
{
    /// <summary>
    /// Decorates any interface or class  T with performance logging
    /// </summary>
    class PerformanceLoggingDecorator<T> where T : class
    {
        private ProxyGenerator _generator = new ProxyGenerator();
        private ProxyGenerationOptions _options = new ProxyGenerationOptions { /*Selector = new TimeFixSelector()*/ };
 
        /// <summary>
        /// Returns the service decorated with performance logging on all public
        /// methods and properties
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public T Decorate(T service)
        {
            return (T)_generator.CreateClassProxyWithTarget(typeof(T), service, _options);
        }
    }
}