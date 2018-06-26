namespace Axle.Application.Modularity
{
    public interface IModuleExporter
    {
        IModuleExporter Export<T>(T instance, string name);
        IModuleExporter Export<T>(T instance);
    }
}