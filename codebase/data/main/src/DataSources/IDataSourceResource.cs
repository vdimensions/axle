using System;

namespace Axle.Data.DataSources
{
    public interface IDataSourceResource : IDataSourceAware, IDisposable
    {
    }
    public interface IDataSourceResource<T> : IDataSourceResource where T: IDisposable
    {
        T WrappedInstance { get; }
    }
}