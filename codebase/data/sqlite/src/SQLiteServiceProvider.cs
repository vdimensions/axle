using System.Data;
using System.IO;

using SQLiteConnection      = System.Data.SQLite.SQLiteConnection;
using SQLiteTransaction     = System.Data.SQLite.SQLiteTransaction;
using SQLiteCommand         = System.Data.SQLite.SQLiteCommand;
using SQLiteCommandBuilder  = System.Data.SQLite.SQLiteCommandBuilder;
using SQLiteDataAdapter     = System.Data.SQLite.SQLiteDataAdapter;
using SQLiteDataReader      = System.Data.SQLite.SQLiteDataReader;
using SQLiteParameter       = System.Data.SQLite.SQLiteParameter;
using Axle.Data.Common;
using Axle.References;


namespace Axle.Data.SQLite
{
    public sealed class SQLiteServiceProvider : DbServiceProvider<
            SQLiteConnection,
            SQLiteTransaction,
            SQLiteCommand,
            SQLiteCommandBuilder,
            SQLiteDataAdapter,
            SQLiteDataReader,
            SQLiteParameter,
            SQLiteType>
    {
        private static DbType SQLiteType2DbType(SQLiteType type)
        {
            switch (type)
            {
                case SQLiteType.Integer:
                    return DbType.Int64;
                case SQLiteType.Blob:
                    // TODO: not sure if right
                    return DbType.Binary;
                case SQLiteType.Double:
                    return DbType.Double;
                case SQLiteType.Text:
                    return DbType.String;
                default:
                    return DbType.Object;
            }
        }

        public const string Name = "System.Data.SQLite";

        /// <summary>
        /// Name of the SQLite dialect as recognized by the Axle framework
        /// </summary>
        public const string Dialect = "SQLite";

        public static SQLiteServiceProvider Instance => Singleton<SQLiteServiceProvider>.Instance;

        private readonly IDbParameterValueSetter<SQLiteParameter, SQLiteType> _parameterValueSetter = new SQLiteParameterValueSetter();

        private SQLiteServiceProvider() : base(Name, Dialect) { }

        public FileInfo CreateFile(string path)
        {
            SQLiteConnection.CreateFile(path);
            return new FileInfo(path);
        }

        protected override SQLiteConnection CreateConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }

        protected override SQLiteTransaction CreateTransaction(SQLiteConnection connection, IsolationLevel? isolationLevel)
        {
            return isolationLevel.HasValue
                ? connection.BeginTransaction(isolationLevel.Value)
                : connection.BeginTransaction();
        }

        protected override SQLiteCommand CreateCommand(
            string queryString, 
            CommandType? commandType, 
            int? commandTimeout,
            SQLiteConnection connection,
            SQLiteTransaction transaction)
        {
            var command = transaction != null
                ? new SQLiteCommand(queryString, connection, transaction)
                : new SQLiteCommand(queryString, connection);
            command.CommandType = commandType.GetValueOrDefault(CommandType.Text);
            if (commandTimeout.HasValue)
            {
                command.CommandTimeout = commandTimeout.Value;
            }
            return command;
        }

        protected override SQLiteCommandBuilder CreateCommandBuilder(SQLiteDataAdapter dataAdapter) => new SQLiteCommandBuilder(dataAdapter);

        protected override SQLiteDataAdapter CreateDataAdapter(SQLiteCommand command) => new SQLiteDataAdapter(command);

        protected override SQLiteDataReader CreateDataReader(SQLiteCommand command, CommandBehavior? behavior)
        {
            return behavior.HasValue ? command.ExecuteReader(behavior.Value) : command.ExecuteReader();
        }

        protected override SQLiteParameter CreateDbParameter(string name, SQLiteType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new SQLiteParameter { ParameterName = name, DbType = SQLiteType2DbType(type.Value), Size = size ?? -1, Direction = direction }
                : new SQLiteParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }
        protected override SQLiteParameter CreateDbParameter(string name, DbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new SQLiteParameter { ParameterName = name, DbType = type.Value, Size = size ?? -1, Direction = direction }
                : new SQLiteParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }

        protected override IDbParameterValueSetter<SQLiteParameter, SQLiteType> ParameterValueSetter => _parameterValueSetter;
    }
}
