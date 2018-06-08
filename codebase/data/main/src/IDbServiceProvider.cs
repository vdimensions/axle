using System.Data;
using System.Data.Common;


namespace Axle.Data
{
    public interface IDbServiceProvider
    {
        //IDataConnection CreateConnection(string connectionString);
        //IDataConnection CreateConnection(ConnectionStringSettings connectionString);

        IDbConnection CreateConnection(string connectionString);

        IDbTransaction CreateTransaction(IDbConnection connection, IsolationLevel? isolationLevel);

        IDbCommand CreateCommand(string queryString, CommandType? commandType, int? commandTimeout, IDbConnection connection, IDbTransaction transaction);

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        IDbDataAdapter CreateDataAdapter(DbCommand command);
        #endif

        DbDataReader CreateDataReader(IDbCommand command, CommandBehavior? behavior);

        DbParameter CreateParameter(string name, DbType? type, int? size, ParameterDirection direction, object value);

        string ProviderName { get; }
        string DialectName { get; }
    }
}