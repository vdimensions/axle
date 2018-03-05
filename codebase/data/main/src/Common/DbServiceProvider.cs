using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

using Axle.Verification;


namespace Axle.Data.Common
{
    #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
    /// <summary>
    /// A base class for creating a factory for data access components.
    /// </summary>
    /// <typeparam name="TDbConnection"></typeparam>
    /// <typeparam name="TDbTransaction"></typeparam>
    /// <typeparam name="TDbCommand"></typeparam>
    /// <typeparam name="TDbDataAdapter"></typeparam>
    /// <typeparam name="TDbDataReader"></typeparam>
    /// <typeparam name="TDbParameter"></typeparam>
    /// <typeparam name="TDbType"></typeparam>
    #else
    /// <summary>
    /// A base class for creating a factory for data access components.
    /// </summary>
    /// <typeparam name="TDbConnection"></typeparam>
    /// <typeparam name="TDbTransaction"></typeparam>
    /// <typeparam name="TDbCommand"></typeparam>
    /// <typeparam name="TDbDataReader"></typeparam>
    /// <typeparam name="TDbParameter"></typeparam>
    /// <typeparam name="TDbType"></typeparam>
    #endif
    #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
    [Serializable]
    #endif
    public abstract class DbServiceProvider<
            TDbConnection, 
            TDbTransaction,
            TDbCommand,
            #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
            TDbDataAdapter,
            #endif
            TDbDataReader,
            TDbParameter,
            TDbType> : IDbServiceProvider, IDbParameterFactory<TDbParameter, TDbType>
        where TDbConnection: class, IDbConnection
        where TDbTransaction: class, IDbTransaction 
        where TDbCommand: DbCommand, IDbCommand
        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        where TDbDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter
        #endif
        where TDbDataReader: DbDataReader, IDataReader
        where TDbParameter: DbParameter
        where TDbType: struct
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _providerName;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _dialectName;

        protected DbServiceProvider(string providerName, string dialectName)
        {
            _providerName = providerName.VerifyArgument(nameof(providerName)).IsNotNullOrEmpty();
            _dialectName = dialectName.VerifyArgument(nameof(dialectName)).IsNotNull();
        }
        
        #region Connection
        //IDataConnection IDbServiceProvider.CreateConnection(string connectionString)
        //{
        //    return new DbConnection<TDbConnection, TDbTransaction, TDbCommand, TDbDataAdapter, TDbDataReader, TDbParameter, TDbType>(this, CreateConnection(connectionString));
        //}
        //IDataConnection IDbServiceProvider.CreateConnection(ConnectionStringSettings connectionString/*, IDbConnector connector*/)
        //{
        //    return new DbConnection<TDbConnection, TDbTransaction, TDbCommand, TDbDataAdapter, TDbDataReader, TDbParameter, TDbType>(
        //        this, 
        //        CreateConnection(connectionString.ConnectionString));
        //}
        protected abstract TDbConnection CreateConnection(string connectionString);
        #endregion

        #region Transaction
        protected abstract TDbTransaction CreateTransaction(TDbConnection connection, IsolationLevel? isolationLevel);
        IDbTransaction IDbServiceProvider.CreateTransaction(IDbConnection connection, IsolationLevel? isolationLevel)
        {
            var conn = connection.VerifyArgument(nameof(connection)).IsNotNull().IsOfType<TDbConnection>().Value;
            return CreateTransaction(conn, isolationLevel);
        }
        #endregion

        #region Command
        protected abstract TDbCommand CreateCommand(string queryString, CommandType? commandType, int? commandTimeout, TDbConnection connection, TDbTransaction transaction);
        IDbCommand IDbServiceProvider.CreateCommand(string queryString, CommandType? commandType, int? commandTimeout, IDbConnection connection, IDbTransaction transaction)
        {
            var conn = connection.VerifyArgument(nameof(connection)).IsNotNull().IsOfType<TDbConnection>().Value;
            var tran = transaction?.VerifyArgument(nameof(transaction)).IsOfType<TDbTransaction>().Value;
            return CreateCommand(queryString, commandType, commandTimeout, conn, tran);
        }
        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        protected abstract TDbDataAdapter CreateDataAdapter(TDbCommand command);
        IDbDataAdapter IDbServiceProvider.CreateDataAdapter(DbCommand command)
        {
            return CreateDataAdapter(command.VerifyArgument(nameof(command)).IsNotNull().IsOfType<TDbCommand>());
        }
        #endif

        protected abstract TDbDataReader CreateDataReader(TDbCommand command, CommandBehavior? behavior);
        DbDataReader IDbServiceProvider.CreateDataReader(IDbCommand command, CommandBehavior? behavior)
        {
            return CreateDataReader(command.VerifyArgument(nameof(command)).IsNotNull().IsOfType<TDbCommand>(), behavior);
        }

        protected abstract TDbParameter CreateDbParameter(string name, TDbType? type, int? size, ParameterDirection direction);
        protected abstract TDbParameter CreateDbParameter(string name, DbType? type, int? size, ParameterDirection direction);
        TDbParameter IDbParameterFactory<TDbParameter, TDbType>.CreateDbParameter(string name, TDbType? type, int? size, ParameterDirection direction, object value)
        {
            var parameter = CreateDbParameter(name, type, size, direction);
            if (value != null)
            {
                if (type.HasValue)
                {
                    var setter = ParameterValueSetter;
                    if (setter != null)
                    {
                        setter.SetValue(parameter, type.Value, value);
                    }
                    else
                    {
                        parameter.Value = value;
                    }
                }
                else
                {
                    parameter.Value = value;
                }
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
            return parameter;
        }
        DbParameter IDbServiceProvider.CreateParameter(string name, DbType? type, int? size, ParameterDirection direction, object value)
        {
            var parameter = CreateDbParameter(name, type, size, direction);
            if (value != null)
            {
                if (type.HasValue)
                {
                    var setter = ParameterValueSetter;
                    if (setter != null)
                    {
                        setter.SetValue(parameter, type.Value, value);
                    }
                    else
                    {
                        parameter.Value = value;
                    }
                }
                else
                {
                    parameter.Value = value;
                }
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
            return parameter;
        }
        #endregion

        //[CanBeNull]
        protected virtual IDbParameterValueSetter<TDbParameter, TDbType> ParameterValueSetter => this as IDbParameterValueSetter<TDbParameter, TDbType>;

        //[CanBeNull(false)]
        public string ProviderName => _providerName;

        //[CanBeNull(false)]
        public string DialectName => _dialectName;
    }
}