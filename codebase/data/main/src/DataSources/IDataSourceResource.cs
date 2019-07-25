using System;

namespace Axle.Data.DataSources
{
    public interface IDataSourceResource : IDataSourceObject, IDisposable
    {
    }
    public interface IDataSourceResource<T> : IDataSourceResource where T: IDisposable
    {
        T WrappedInstance { get; }
    }
}