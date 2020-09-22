using System.Data;
using System.Data.Common;

namespace Axle.Data
{
    public interface IDbServiceProvider
    {
        IDbConnection CreateConnection(string connectionString);

        IDbTransaction CreateTransaction(IDbConnection connection, IsolationLevel? isolationLevel);

        DbCommand CreateCommand(string queryString, CommandType? commandType, int? commandTimeout, IDbConnection connection, IDbTransaction transaction);

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        DbDataAdapter CreateDataAdapter(IDbCommand command);
        DbCommandBuilder CreateCommandBuilder(DbDataAdapter dataAdapter);
        #endif

        DbDataReader CreateDataReader(IDbCommand command, CommandBehavior? behavior);

        DbParameter CreateParameter(string name, DbType? type, int? size, ParameterDirection direction, object value);

        string ProviderName { get; }
        string DialectName { get; }
    }
}