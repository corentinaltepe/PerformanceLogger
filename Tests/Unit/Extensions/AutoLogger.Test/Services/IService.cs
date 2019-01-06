namespace PerformanceLogger.Extensions.DependencyInjection.Test.Services
{
    public interface IService
    {
        void VoidMethodNoArgument();
        void VoidMethodOneArgument(int argument);
    }
}