using System.Data;
using System.Data.Common;
using Axle.Modularity;

namespace Axle.Data
{
    [Module]
    [Requires(typeof(DbServiceProviderRegistry))]
    public abstract class DatabaseServiceProviderModule : IDbServiceProvider
    {
        internal IDbServiceProvider Provider { get; }

        protected DatabaseServiceProviderModule(IDbServiceProvider provider)
        {
            Provider = provider;
        }

        string IDbServiceProvider.ProviderName => Provider.ProviderName;

        string IDbServiceProvider.DialectName => Provider.DialectName;

        IDbCommand IDbServiceProvider.CreateCommand(string queryString, CommandType? commandType, int? commandTimeout, IDbConnection connection, IDbTransaction transaction)
        {
            return Provider.CreateCommand(queryString, commandType, commandTimeout, connection, transaction);
        }

        IDbConnection IDbServiceProvider.CreateConnection(string connectionString) => Provider.CreateConnection(connectionString);

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        IDbDataAdapter IDbServiceProvider.CreateDataAdapter(DbCommand command) => Provider.CreateDataAdapter(command);
        #endif

        DbDataReader IDbServiceProvider.CreateDataReader(IDbCommand command, CommandBehavior? behavior)
        {
            return Provider.CreateDataReader(command, behavior);
        }

        DbParameter IDbServiceProvider.CreateParameter(string name, DbType? type, int? size, ParameterDirection direction, object value)
        {
            return Provider.CreateParameter(name, type, size, direction, value);
        }

        IDbTransaction IDbServiceProvider.CreateTransaction(IDbConnection connection, IsolationLevel? isolationLevel)
        {
            return Provider.CreateTransaction(connection, isolationLevel);
        }
    }
}
