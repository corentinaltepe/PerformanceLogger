using System.Threading;

namespace Sample.SampleServices
{
    /// <summary>
    /// Mock of a service implementing IService to test DependencyInjection extension
    /// </summary>
    class ServiceB : IService
    {
        public void ExecuteSomething()
        {
            // Simulate a long executing operation
            Thread.Sleep(25);
        }

        public void ExecuteSomethingWithArgument(string arg1)
        {
            // Do nothing
        }

        public int FindSomething()
        {
            return 0;
        }

        public int FindSomethingWithArguments(string arg1, double arg2)
        {
            return (int)(2 * arg2);
        }
    }
}