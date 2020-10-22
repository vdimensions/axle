using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Axle.Data.Extensions.DbCommand;
using Axle.Verification;

namespace Axle.Data.DataSources
{
    internal delegate DbCommand CommandFactory(string commandText, IDataSourceConnection connection);

    internal sealed class DataSourceCommand : IDataSourceCommand
    {
        private readonly IDbServiceProvider _provider;
        private readonly CommandFactory _commandFactory;

        internal DataSourceCommand(IDataSource dataSource, IDbServiceProvider provider, string commandText, CommandFactory commandFactory)
        {
            DataSource = dataSource;
            _provider = provider;
            _commandFactory = commandFactory;
            CommandText = commandText;
        }

        private TResult Execute<TResult>(Func<DbCommand, TResult> operation, IDataSourceConnection connection, out int returnValue, IEnumerable<IDataParameter> parameters)
        {
            using (var command = _commandFactory(CommandText, connection))
            {
                IDataParameter returnValueParameter = null;
                foreach (var dataParameter in parameters)
                {
                    command.Parameters.Add(dataParameter);
                    if (dataParameter.Direction == ParameterDirection.ReturnValue)
                    {
                        returnValueParameter = dataParameter;
                    }
                }
                returnValue = 0;
                TResult result;
                if (command.CommandType == CommandType.StoredProcedure)
                {
                    result = operation(command);
                    #warning use return value output parameter
                    if (returnValueParameter != null)
                    {
                        var outValue = returnValueParameter.Value;
                        returnValue = (outValue is DBNull || outValue == null) ? 0 : (int) outValue;
                    }
                }
                else
                {
                    result = operation(command);
                }

                return result;
            }
        }

        public int ExecuteNonQuery(IDataSourceConnection connection, params IDataParameter[] parameters)
        {
            connection.VerifyArgument(nameof(connection)).IsNotNull();
            return Execute(c => c.ExecuteNonQuery(), connection, out _, parameters);
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public int ExecuteQuery(IDataSourceConnection connection, DataSet results, params IDataParameter[] parameters)
        {
            return Execute(
                command => DbCommandExtensions.FillDataSet(
                    command, 
                    Verifier.IsNotNull(Verifier.VerifyArgument(results, nameof(results))).Value, 
                    () => _provider.CreateDataAdapter(command)),
                connection.VerifyArgument(nameof(connection)).IsNotNull().Value,
                out _,
                parameters);
        }
        public int ExecuteQuery(IDataSourceConnection connection, DataTable results, params IDataParameter[] parameters)
        {
            return Execute(
                command => DbCommandExtensions.FillDataTable(
                    command, 
                    Verifier.IsNotNull(Verifier.VerifyArgument(results, nameof(results))).Value, 
                    () => _provider.CreateDataAdapter(command)),
                connection.VerifyArgument(nameof(connection)).IsNotNull().Value,
                out _,
                parameters);
        }
        #endif

        public void ExecuteReader(IDataSourceConnection connection, CommandBehavior behavior, Action<DbDataReader> readAction, params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(readAction, nameof(readAction)));
            Execute<object>(
                command =>
                {
                    using (var reader = _provider.CreateDataReader(command, behavior))
                    {
                        readAction(reader);
                    }
                    return null;
                },
                connection.VerifyArgument(nameof(connection)).IsNotNull().Value,
                out _,
                parameters);
        }

        public object ExecuteScalar(IDataSourceConnection connection, params IDataParameter[] parameters)
        {
            connection.VerifyArgument(nameof(connection)).IsNotNull();
            return Execute(command => command.ExecuteScalar(), connection, out _, parameters);
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public int ExecuteStoredProcedure(IDataSourceConnection connection, DataSet results, params IDataParameter[] parameters)
        {
            Execute<object>(
                command => DbCommandExtensions.FillDataSet(
                    command, 
                    Verifier.IsNotNull(Verifier.VerifyArgument(results, nameof(results))), 
                    () => _provider.CreateDataAdapter(command)),
                connection.VerifyArgument(nameof(connection)).IsNotNull().Value,
                out var returnValue,
                parameters);
            return returnValue;
        }
        public int ExecuteStoredProcedure(IDataSourceConnection connection, DataTable results, params IDataParameter[] parameters)
        {
            Execute<object>(
                command => DbCommandExtensions.FillDataTable(
                    command, 
                    Verifier.IsNotNull(Verifier.VerifyArgument(results, nameof(results))), 
                    () => _provider.CreateDataAdapter(command)),
                connection.VerifyArgument(nameof(connection)).IsNotNull().Value,
                out var returnValue,
                parameters);
            return returnValue;
        }
        #endif

        public override string ToString() => CommandText;

        public string CommandText { get; }
        public IDataSource DataSource { get; }
    }
}