using System;
using System.Data;

namespace Axle.Data.DataSources
{
    public interface IDataSourceTransaction : IDataSourceResource<IDbTransaction>, IDisposable
    {
        void Commit();
        void Rollback();

        IDataSourceConnection Connection { get; }
    }
}