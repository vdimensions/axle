using System;
using System.Data;

namespace Axle.Data.DataSources
{
    public interface IDataSourceConnection : IDataSourceResource<IDbConnection>, IDisposable
    {
        IDataSourceTransaction BeginTransaction(IsolationLevel isolationLevel);

        IDataSourceTransaction CurrentTransaction { get; }
    }
}