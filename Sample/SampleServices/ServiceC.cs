namespace Sample.SampleServices
{
    /// <summary>
    /// Mock of a service to test DependencyInjection extension
    /// </summary>
    class ServiceC
    {
        public void ExecuteSomething()
        {
            // Do nothing
        }

        public int FindSomethingWithArguments(string arg1, double arg2)
        {
            return (int)(17 * arg2);
        }
    }
}