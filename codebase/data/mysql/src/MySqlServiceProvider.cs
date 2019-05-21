using System.Data;
using Axle.Data.Common;
using MySql.Data.MySqlClient;


namespace Axle.Data.MySql
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class MySqlServiceProvider : DbServiceProvider<
        MySqlConnection,
        MySqlTransaction,
        MySqlCommand,
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        MySqlDataAdapter,
        #endif
        MySqlDataReader,
        MySqlParameter,
        MySqlDbType>
    {
        public const string Name = "MySql.Data.MySqlClient";

        private readonly MySqlParameterValueSetter _parameterValueSetter;

        public MySqlServiceProvider(string dialect) : base(Name, dialect)
        {
            _parameterValueSetter = new MySqlParameterValueSetter();
        }

        protected override MySqlConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        protected override MySqlTransaction CreateTransaction(MySqlConnection connection, IsolationLevel? isolationLevel)
        {
            return isolationLevel.HasValue
               ? connection.BeginTransaction(isolationLevel.Value)
               : connection.BeginTransaction();
        }

        protected override MySqlCommand CreateCommand(
            string queryString,
            CommandType? commandType,
            int? commandTimeout,
            MySqlConnection connection,
            MySqlTransaction transaction)
        {
            var command = transaction != null
                ? new MySqlCommand(queryString, connection, transaction)
                : new MySqlCommand(queryString, connection);
            command.CommandType = commandType.GetValueOrDefault(CommandType.Text);
            if (commandTimeout.HasValue)
            {
                command.CommandTimeout = commandTimeout.Value;
            }
            return command;
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected override MySqlDataAdapter CreateDataAdapter(MySqlCommand command)
        {
            return new MySqlDataAdapter(command);
        }
        #endif

        protected override MySqlDataReader CreateDataReader(MySqlCommand command, CommandBehavior? behavior)
        {
            return behavior.HasValue ? command.ExecuteReader(behavior.Value) : command.ExecuteReader();
        }

        protected override MySqlParameter CreateDbParameter(string name, MySqlDbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new MySqlParameter { ParameterName = name, MySqlDbType = type.Value, Size = size ?? -1, Direction = direction }
                : new MySqlParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }
        protected override MySqlParameter CreateDbParameter(string name, DbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new MySqlParameter { ParameterName = name, DbType = type.Value, Size = size ?? -1, Direction = direction }
                : new MySqlParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }

        protected override IDbParameterValueSetter<MySqlParameter, MySqlDbType> ParameterValueSetter => _parameterValueSetter;
    }
}
