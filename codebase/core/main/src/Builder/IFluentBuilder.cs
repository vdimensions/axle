#if NETSTANDARD || NET20_OR_NEWER
namespace Axle.Builder
{
    public interface IFluentBuilder<T>
    {
        T Build();

        T[] BuildMany(int count);
    }

    public interface IFluentBuilder<T, TArgs> : IFluentBuilder<T>
    {
        TArgs Args { get; }
    }
}
#endif