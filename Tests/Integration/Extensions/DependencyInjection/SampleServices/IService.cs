namespace DependencyInjection.SampleServices
{
    /// <summary>
    /// Mock of an interface that will be called by the dependency injection service
    /// to resolve and decorate with performance logger.
    /// </summary>
    interface IService
    {
        void ExecuteSomething();
        void ExecuteSomethingWithArgument(string arg1);
        int FindSomething();
        int FindSomethingWithArguments(string arg1, double arg2);
    }
}