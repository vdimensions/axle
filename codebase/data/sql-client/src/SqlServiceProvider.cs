﻿using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

using Axle.Data.Common;


namespace Axle.Data.SqlClient
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class SqlServiceProvider : DbServiceProvider<
            SqlConnection, 
            SqlTransaction, 
            SqlCommand,
            #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
            SqlDataAdapter, 
            #endif
            SqlDataReader, 
            SqlParameter,
            SqlDbType>
    {
        public const string Name = "System.Data.SqlClient";
        public const string Dialect = "MS-SQL";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SqlParameterValueSetter _parameterValueSetter;

        public SqlServiceProvider() : this(Name) { }
        internal SqlServiceProvider(string name) : base(name, Dialect)
        {
            _parameterValueSetter = new SqlParameterValueSetter();
        }
        
        protected override SqlConnection CreateConnection(string connectionString) { return new SqlConnection(connectionString); }
        
        protected override SqlTransaction CreateTransaction(SqlConnection connection, IsolationLevel? isolationLevel)
        {
            return isolationLevel.HasValue
                ? connection.BeginTransaction(isolationLevel.Value)
                : connection.BeginTransaction();
        }
        
        protected override SqlCommand CreateCommand(
            string queryString, 
            CommandType? commandType, 
            int? commandTimeout, 
            SqlConnection connection, 
            SqlTransaction transaction)
        {
            var command = transaction != null
                ? new SqlCommand(queryString, connection, transaction)
                : new SqlCommand(queryString, connection);
            command.CommandType = commandType.GetValueOrDefault(CommandType.Text);
            if (commandTimeout.HasValue)
            {
                command.CommandTimeout = commandTimeout.Value;
            }
            return command;
        }

        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        protected override SqlDataAdapter CreateDataAdapter(SqlCommand command) => new SqlDataAdapter(command);
        #endif
        
        protected override SqlDataReader CreateDataReader(SqlCommand command, CommandBehavior? behavior)
        {
            return behavior.HasValue ? command.ExecuteReader(behavior.Value) : command.ExecuteReader();
        }
        
        protected override SqlParameter CreateDbParameter(string name, SqlDbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new SqlParameter { ParameterName = name, SqlDbType = type.Value, Size = size ?? -1, Direction = direction }
                : new SqlParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }
        protected override SqlParameter CreateDbParameter(string name, DbType? type, int? size, ParameterDirection direction)
        {
            return type.HasValue
                ? new SqlParameter { ParameterName = name, DbType = type.Value, Size = size ?? -1, Direction = direction }
                : new SqlParameter { ParameterName = name, Size = size ?? -1, Direction = direction };
        }

        protected override IDbParameterValueSetter<SqlParameter, SqlDbType> ParameterValueSetter => _parameterValueSetter;
    }
}