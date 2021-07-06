using System;
using System.Data;

namespace Axle.Data.DataSources
{
    internal sealed class DataSourceConnection : IDataSourceConnection
    {
        private readonly IDbServiceProvider _provider;
        private readonly IDbConnection _connection;

        public DataSourceConnection(IDbServiceProvider provider, IDataSource dataSource)
        {
            _provider = provider;
            DataSource = dataSource;
            _connection = provider.CreateConnection(dataSource.ConnectionString);
            _connection.Open();
        }

        public IDataSourceTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            if (CurrentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already started for this connection.");
            }

            var dbTransaction = _provider.CreateTransaction(_connection, isolationLevel);
            return CurrentTransaction = new DataSourceTransaction(dbTransaction, this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                CurrentTransaction?.Dispose();
                CurrentTransaction = null;
                _connection?.Dispose();
            }
        }
        void IDisposable.Dispose() => Dispose(true);

        public IDataSourceTransaction CurrentTransaction { get; private set; }
        public IDataSource DataSource { get; }
        IDbConnection IDataSourceResource<IDbConnection>.WrappedInstance => _connection;
    }
}