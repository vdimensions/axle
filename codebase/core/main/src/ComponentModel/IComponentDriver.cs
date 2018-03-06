namespace Axle.ComponentModel
{
    public interface IComponentDriver
    {
        object Resolve();
    }
    public interface IComponentDriver<T> : IComponentDriver
    {
        T Resolve();
    }
}
