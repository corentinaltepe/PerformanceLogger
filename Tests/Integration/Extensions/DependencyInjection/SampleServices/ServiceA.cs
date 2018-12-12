namespace DependencyInjection.SampleServices
{
    /// <summary>
    /// Mock of a service implementing IService to test DependencyInjection extension
    /// </summary>
    class ServiceA : IService
    {
        public void ExecuteSomething()
        {
            // Do nothing
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