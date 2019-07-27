#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Data;
using System.Data.Odbc;

using Axle.Data.Common;


namespace Axle.Data.Odbc
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class OdbcServiceProvider : DbServiceProvider<
        OdbcConnection,
        OdbcTransaction,
        OdbcCommand,
        OdbcCommandBuilder,
        OdbcDataAdapter,
        OdbcDataReader,
        OdbcParameter,
        OdbcType>
    {
        public const string Name = "System.Data.Odbc";

        private readonly OdbcParameterValueSetter _parameterValueSetter = new OdbcParameterValueSetter();

        public OdbcServiceProvider(string dialect) : base(Name, dialect) { }
        
        protected override OdbcConnection CreateConnection(string connectionString) => new OdbcConnection(connectionString);

        protected override OdbcTransaction CreateTransaction(OdbcConnection connection, IsolationLevel? isolationLevel)
        {
            return isolationLevel.HasValue
               ? connection.BeginTransaction(isolationLevel.Value)
               : connection.BeginTransaction();
        }

        protected override OdbcCommand CreateCommand(
            string queryString, 
            CommandType? commandType, 
            int? commandTimeout,
            OdbcConnection connection,
            OdbcTransaction transaction)
        {
            var command = transaction != null
                ? new OdbcCommand(queryString, connection, transaction)
                : new OdbcCommand(queryString, connection);
            command.CommandType = commandType.GetValueOrDefault(CommandType.Text);
            if (commandTimeout.HasValue)
            {
                command.CommandTimeout = commandTimeout.Value;
            }
            return command;
        }

        protected override OdbcCommandBuilder CreateCommandBuilder(OdbcDataAdapter dataAdapter) => new OdbcCommandBuilder(dataAdapter);

        protected override OdbcDataAdapter CreateDataAdapter(OdbcCommand command) => new OdbcDataAdapter(command);

        protected override OdbcDataReader CreateDataReader(OdbcCommand command, CommandBehavior? behavior)
        {
            return behavior.HasValue ? command.ExecuteReader(behavior.Value) : command.ExecuteReader();
        }

        protected override OdbcParameter CreateDbParameter(string name, OdbcType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new OdbcParameter { ParameterName = name, OdbcType = type.Value, Size = size ?? -1, Direction = direction }
                : new OdbcParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }
        protected override OdbcParameter CreateDbParameter(string name, DbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new OdbcParameter { ParameterName = name, DbType = type.Value, Size = size ?? -1, Direction = direction }
                : new OdbcParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }

        protected override IDbParameterValueSetter<OdbcParameter, OdbcType> ParameterValueSetter => _parameterValueSetter;
    }
}
#endif