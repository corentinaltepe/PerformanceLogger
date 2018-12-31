using System.Threading;
using PerformanceLogger;

namespace Sample.SampleServices
{
    /// <summary>
    /// Mock of a service implementing IService to test DependencyInjection extension
    /// </summary>
    class ServiceA : IService
    {
        private readonly IPerformanceLogger _perfLogger;
        public ServiceA(IPerformanceLogger perfLogger)
        {
            _perfLogger = perfLogger;
        }

        public int GetProperty => 22;
        public int GetSetProperty { get;set; }

        public void ExecuteSomething()
        {
            var tracker = _perfLogger.Start("servicea_execute_something");

            // Simulate a long executing operation
            Thread.Sleep(25);

            tracker.End();
        }

        public void ExecuteSomethingWithArgument(string arg1)
        {
            // Do nothing
        }

        public int FindSomething()
        {
            return 21;
        }

        public int FindSomethingWithArguments(string arg1, double arg2)
        {
            return (int)(52 * arg2);
        }
    }
}