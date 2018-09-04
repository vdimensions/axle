using System.Data;

using Npgsql;
using NpgsqlTypes;

using Axle.Data.Common;


namespace Axle.Data.Npgsql
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class NpgsqlServiceProvider : DbServiceProvider<
        NpgsqlConnection,
        NpgsqlTransaction,
        NpgsqlCommand,
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        NpgsqlDataAdapter,
        #endif
        NpgsqlDataReader,
        NpgsqlParameter,
        NpgsqlDbType>
    {
        public const string Name = "Npgsql";
        public const string Dialect = "PostgreSQL";

        private readonly IDbParameterValueSetter<NpgsqlParameter, NpgsqlDbType> _parameterValueSetter = new NpgsqlParameterValueSetter();

        internal NpgsqlServiceProvider() : this(Name) { }
        internal NpgsqlServiceProvider(string name) : base(name, Dialect) { }

        protected override NpgsqlConnection CreateConnection(string connectionString) => new NpgsqlConnection(connectionString);

        protected override NpgsqlTransaction CreateTransaction(NpgsqlConnection connection, IsolationLevel? isolationLevel)
        {
            return isolationLevel.HasValue
               ? connection.BeginTransaction(isolationLevel.Value)
               : connection.BeginTransaction();
        }

        protected override NpgsqlCommand CreateCommand(
            string queryString, 
            CommandType? commandType, 
            int? commandTimeout,
            NpgsqlConnection connection,
            NpgsqlTransaction transaction)
        {
            var command = transaction != null
                ? new NpgsqlCommand(queryString, connection, transaction)
                : new NpgsqlCommand(queryString, connection);
            command.CommandType = commandType.GetValueOrDefault(CommandType.Text);
            if (commandTimeout.HasValue)
            {
                command.CommandTimeout = commandTimeout.Value;
            }
            return command;
        }

        #if NETFRAMEWORK || NETSTANDARD2_0_OR_NEWER
        protected override NpgsqlDataAdapter CreateDataAdapter(NpgsqlCommand command) => new NpgsqlDataAdapter(command);
        #endif

        protected override NpgsqlDataReader CreateDataReader(NpgsqlCommand command, CommandBehavior? behavior)
        {
            return behavior.HasValue ? command.ExecuteReader(behavior.Value) : command.ExecuteReader();
        }

        protected override NpgsqlParameter CreateDbParameter(string name, NpgsqlDbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new NpgsqlParameter(name, type.Value) { Size = size ?? -1, Direction = direction }
                : new NpgsqlParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }
        protected override NpgsqlParameter CreateDbParameter(string name, DbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new NpgsqlParameter { ParameterName = name, DbType = type.Value, Size = size ?? -1, Direction = direction }
                : new NpgsqlParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }

        protected override IDbParameterValueSetter<NpgsqlParameter, NpgsqlDbType> ParameterValueSetter => _parameterValueSetter;
    }
}
