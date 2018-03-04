using System.Data;
using System.Data.OleDb;

using Axle.Data.Common;


namespace Axle.Data.OleDb
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class OleDbServiceProvider : DbServiceProvider<
        OleDbConnection,
        OleDbTransaction,
        OleDbCommand,
        OleDbDataAdapter,
        OleDbDataReader,
        OleDbParameter,
        OleDbType>/*,
        IDbParameterValueSetter<OleDbType>*/
    {
        public const string Name = "System.Data.OleDb";

        public OleDbServiceProvider(string dialect) : base(Name, dialect) { }

        protected override OleDbConnection CreateConnection(string connectionString)
        {
            return new OleDbConnection(connectionString);
        }

        protected override OleDbTransaction CreateTransaction(OleDbConnection connection, IsolationLevel? isolationLevel)
        {
            return isolationLevel.HasValue
               ? connection.BeginTransaction(isolationLevel.Value)
               : connection.BeginTransaction();
        }

        protected override OleDbCommand CreateCommand(
            string queryString, 
            CommandType? commandType, 
            int? commandTimeout,
            OleDbConnection connection,
            OleDbTransaction transaction)
        {
            var command = transaction != null
                ? new OleDbCommand(queryString, connection, transaction)
                : new OleDbCommand(queryString, connection);
            command.CommandType = commandType.GetValueOrDefault(CommandType.Text);
            if (commandTimeout.HasValue)
            {
                command.CommandTimeout = commandTimeout.Value;
            }
            return command;
        }

        protected override OleDbDataAdapter CreateDataAdapter(OleDbCommand command)
        {
            return new OleDbDataAdapter(command);
        }

        protected override OleDbDataReader CreateDataReader(OleDbCommand command, CommandBehavior? behavior)
        {
            return behavior.HasValue ? command.ExecuteReader(behavior.Value) : command.ExecuteReader();
        }

        protected override OleDbParameter CreateDbParameter(string name, OleDbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new OleDbParameter { ParameterName = name, OleDbType = type.Value, Size = size ?? -1, Direction = direction }
                : new OleDbParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }
        protected override OleDbParameter CreateDbParameter(string name, DbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new OleDbParameter { ParameterName = name, DbType = type.Value, Size = size ?? -1, Direction = direction }
                : new OleDbParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }

        #region Implementation of IDbParameterValueSetter
        public object SetValue(object value, OleDbType type)
        {
            throw new System.NotImplementedException();
        }

        public object SetValue(object value, DbType type)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
