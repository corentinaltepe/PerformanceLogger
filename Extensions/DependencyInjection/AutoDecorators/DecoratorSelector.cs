using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace PerformanceLogger.Extensions.DependencyInjection.AutoDecorators
{
    class DecoratorSelector : IInterceptorSelector
    {
        private readonly PerformanceLoggerSelector _loggerSelector;
        public DecoratorSelector(PerformanceLoggerSelector loggerSelector)
        {
            _loggerSelector = loggerSelector;
        }

        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            if(!method.IsPublic)
                return new IInterceptor[] {  }.Union( interceptors ).ToArray();

            return new IInterceptor[] { _loggerSelector }.Union( interceptors ).ToArray();
        }
    }
}