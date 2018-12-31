namespace Sample.SampleServices
{
    /// <summary>
    /// Mock of an interface that will be called by the dependency injection service
    /// to resolve and decorate with performance logger.
    /// </summary>
    public interface IService
    {
        int GetProperty { get; }
        int GetSetProperty { get;set; }
        void ExecuteSomething();
        void ExecuteSomethingWithArgument(string arg1);
        int FindSomething();
        int FindSomethingWithArguments(string arg1, double arg2);
    }
}