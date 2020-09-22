using System;
using System.Data;
using System.Data.Common;
using System.IO;

using SqliteConnection      = Microsoft.Data.Sqlite.SqliteConnection;
using SqliteTransaction     = Microsoft.Data.Sqlite.SqliteTransaction;
using SqliteCommand         = Microsoft.Data.Sqlite.SqliteCommand;
using SqliteCommandBuilder  = System.Data.Common.DbCommandBuilder;
using SqliteDataAdapter     = Axle.Data.Sqlite.Microsoft.SqliteDataAdapter;
using SqliteDataReader      = Microsoft.Data.Sqlite.SqliteDataReader;
using SqliteParameter       = Microsoft.Data.Sqlite.SqliteParameter;
using SqliteType            = Microsoft.Data.Sqlite.SqliteType;

using Axle.Data.Common;
using Axle.References;
using Microsoft.Data.Sqlite;


namespace Axle.Data.Sqlite.Microsoft
{
    public sealed class SqliteServiceProvider : DbServiceProvider<
            SqliteConnection,
            SqliteTransaction,
            SqliteCommand,
            SqliteCommandBuilder,
            SqliteDataAdapter,
            SqliteDataReader,
            SqliteParameter,
            SqliteType>
    {
        private static DbType SqliteType2DbType(SqliteType type)
        {
            switch (type)
            {
                case SqliteType.Integer:
                    return DbType.Int64;
                case SqliteType.Blob:
                    // TODO: not sure if right
                    return DbType.Binary;
                case SqliteType.Real:
                    return DbType.Double;
                case SqliteType.Text:
                    return DbType.String;
                default:
                    return DbType.Object;
            }
        }

        public const string Name = "Microsoft.Data.Sqlite";

        /// <summary>
        /// Name of the SQLite dialect as recognized by the Axle framework
        /// </summary>
        public const string Dialect = "SQLite";

        public static SqliteServiceProvider Instance => Singleton<SqliteServiceProvider>.Instance;

        private readonly IDbParameterValueSetter<SqliteParameter, SqliteType> _parameterValueSetter = new SqliteParameterValueSetter();

        private SqliteServiceProvider() : base(Name, Dialect) { }

        public FileInfo CreateFile(string path)
        {
            File.Create(path).Close();
            return new FileInfo(path);
        }

        protected override SqliteConnection CreateConnection(string connectionString)
        {
            return new SqliteConnection(connectionString);
        }

        protected override SqliteTransaction CreateTransaction(SqliteConnection connection, IsolationLevel? isolationLevel)
        {
            return isolationLevel.HasValue
                ? connection.BeginTransaction(isolationLevel.Value)
                : connection.BeginTransaction();
        }

        protected override SqliteCommand CreateCommand(
            string queryString, 
            CommandType? commandType, 
            int? commandTimeout,
            SqliteConnection connection,
            SqliteTransaction transaction)
        {
            var command = transaction != null
                ? new SqliteCommand(queryString, connection, transaction)
                : new SqliteCommand(queryString, connection);
            command.CommandType = commandType.GetValueOrDefault(CommandType.Text);
            if (commandTimeout.HasValue)
            {
                command.CommandTimeout = commandTimeout.Value;
            }
            return command;
        }

        protected override SqliteCommandBuilder CreateCommandBuilder(SqliteDataAdapter dataAdapter) => throw new NotSupportedException();

        protected override SqliteDataAdapter CreateDataAdapter(SqliteCommand command) => new SqliteDataAdapter(command);

        protected override SqliteDataReader CreateDataReader(SqliteCommand command, CommandBehavior? behavior)
        {
            return behavior.HasValue ? command.ExecuteReader(behavior.Value) : command.ExecuteReader();
        }

        protected override SqliteParameter CreateDbParameter(string name, SqliteType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new SqliteParameter { ParameterName = name, DbType = SqliteType2DbType(type.Value), Size = size ?? -1, Direction = direction }
                : new SqliteParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }
        protected override SqliteParameter CreateDbParameter(string name, DbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new SqliteParameter { ParameterName = name, DbType = type.Value, Size = size ?? -1, Direction = direction }
                : new SqliteParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }

        protected override IDbParameterValueSetter<SqliteParameter, SqliteType> ParameterValueSetter => _parameterValueSetter;
    }
}
