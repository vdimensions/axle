using System;

namespace Axle.Data.DataSources
{
    public interface IDataSourceResource : IDisposable
    {
        IDataSource DataSource { get; }
    }
    public interface IDataSourceResource<T> : IDataSourceResource where T: IDisposable
    {
        T WrappedInstance { get; }
    }
}