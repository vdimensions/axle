using System;
using System.Data;

namespace Axle.Data.DataSources
{
    internal sealed class DataSourceTransaction : IDataSourceTransaction
    {
        private readonly IDbTransaction _transaction;

        public DataSourceTransaction(IDbTransaction transaction, IDataSourceConnection connection)
        {
            _transaction = transaction;
            Connection = connection;
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transaction.Dispose();
            }
        }
        void IDisposable.Dispose() => Dispose(true);

        public void Commit() => _transaction.Commit();

        public void Rollback() => _transaction.Rollback();

        public IDataSourceConnection Connection { get; }
        public IDataSource DataSource => Connection.DataSource;
        IDbTransaction IDataSourceResource<IDbTransaction>.WrappedInstance => _transaction;
    }
}