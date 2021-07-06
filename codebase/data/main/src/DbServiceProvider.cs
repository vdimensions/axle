using System.Data;
using System.Data.Common;

using Axle.Modularity;

namespace Axle.Data
{
    /// <summary>
    /// An abstract module class act as a decorator over an existing <see cref="IDbServiceProvider"/> instance
    /// in order to register on behalf of that provider at application boot time.
    /// </summary>
    [Module]
    public abstract class DbServiceProvider : IDbServiceProvider
    {
        private IDbServiceProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbServiceProvider"/> decorator class using the supplied
        /// <paramref name="provider"/> as the decorated instance.
        /// </summary>
        /// <param name="provider">
        /// The actual <see cref="IDbServiceProvider"/> instance to decorate.
        /// </param>
        protected DbServiceProvider(IDbServiceProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc/>
        public DbCommand CreateCommand(string queryString, CommandType? commandType, int? commandTimeout, IDbConnection connection, IDbTransaction transaction)
        {
            return _provider.CreateCommand(queryString, commandType, commandTimeout, connection, transaction);
        }

        /// <inheritdoc/>
        public IDbConnection CreateConnection(string connectionString)
        {
            return _provider.CreateConnection(connectionString);
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        /// <inheritdoc/>
        public DbCommandBuilder CreateCommandBuilder(DbDataAdapter dataAdapter)
        {
            return _provider.CreateCommandBuilder(dataAdapter);
        }

        /// <inheritdoc/>
        public DbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return _provider.CreateDataAdapter(command);
        }
        #endif

        /// <inheritdoc/>
        public DbDataReader CreateDataReader(IDbCommand command, CommandBehavior? behavior)
        {
            return _provider.CreateDataReader(command, behavior);
        }

        /// <inheritdoc/>
        public DbParameter CreateParameter(string name, DbType? type, int? size, ParameterDirection direction, object value)
        {
            return _provider.CreateParameter(name, type, size, direction, value);
        }

        /// <inheritdoc/>
        public IDbTransaction CreateTransaction(IDbConnection connection, IsolationLevel? isolationLevel)
        {
            return _provider.CreateTransaction(connection, isolationLevel);
        }

        /// <inheritdoc/>
        public string ProviderName => _provider.ProviderName;

        /// <inheritdoc/>
        public string DialectName => _provider.DialectName;
    }
}
