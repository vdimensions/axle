using System;
using System.Data;
using System.IO;
#if NETFRAMEWORK
using SqliteConnection  = System.Data.SQLite.SQLiteConnection;
using SqliteTransaction = System.Data.SQLite.SQLiteTransaction;
using SqliteCommand     = System.Data.SQLite.SQLiteCommand;
using SqliteDataAdapter = System.Data.SQLite.SQLiteDataAdapter;
using SqliteDataReader  = System.Data.SQLite.SQLiteDataReader;
using SqliteParameter   = System.Data.SQLite.SQLiteParameter;
using SqliteType        = Axle.Data.Sqlite.SqliteType;
#else
using Microsoft.Data.Sqlite;

using SqliteType        = Microsoft.Data.Sqlite.SqliteType;
using SqliteDataAdapter = Axle.Data.Sqlite.SqliteDataAdapter;
#endif

using Axle.Data.Common;
using Axle.References;


namespace Axle.Data.Sqlite
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
	public sealed class SqliteServiceProvider : DbServiceProvider<
            SqliteConnection,
            SqliteTransaction,
            SqliteCommand,
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
                #if NETSTANDARD
                case SqliteType.Real:
                #else
                case SqliteType.Double:
                #endif
                    return DbType.Double;
                case SqliteType.Text:
                    return DbType.String;
                default:
                    return DbType.Object;
            }
        }

        #if NETFRAMEWORK
        public const string Name = "System.Data.Sqlite";
        #else
        public const string Name = "Microsoft.Data.Sqlite";
        #endif
        /// <summary>
        /// Name of the SQLite dialect as recognized by the Axle framework
        /// </summary>
        public const string Dialect = "SQLite";

        public static SqliteServiceProvider Instance => Singleton<SqliteServiceProvider>.Instance;

        private readonly IDbParameterValueSetter<SqliteParameter, SqliteType> _parameterValueSetter = new SqliteParameterValueSetter();

        private SqliteServiceProvider() : base(Name, Dialect) { }

        public FileInfo CreateFile(string path)
        {
            #if NETFRAMEWORK
            SqliteConnection.CreateFile(path);
            #else
            using (var fs = File.Create(path, 0, FileOptions.WriteThrough)) { }
            #endif
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
