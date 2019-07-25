using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Axle.Data.Extensions.DbCommand;
using Axle.Verification;


namespace Axle.Data.DataSources
{
    internal sealed class DataSourceCommand : IDataSourceCommand
    {
        private readonly IDbServiceProvider _provider;
        private readonly Func<string, DbCommand> _commandFactory;

        internal DataSourceCommand(IDataSource dataSource, IDbServiceProvider provider, string commandText, Func<string, DbCommand> commandFactory)
        {
            DataSource = dataSource;
            _provider = provider;
            _commandFactory = commandFactory;
            CommandText = commandText;
        }

        private TResult Execute<TResult>(Func<DbCommand, TResult> operation, out int returnValue, IEnumerable<IDataParameter> parameters)
        {
            using (var command = _commandFactory(CommandText))
            {
                foreach (var dataParameter in parameters)
                {
                    command.Parameters.Add(dataParameter);
                }
                returnValue = 0;
                TResult result;
                if (command.CommandType == CommandType.StoredProcedure)
                {
                    result = operation(command);
                    #warning use return value output parameter
                    var returnValueParameter = Enumerable.SingleOrDefault(parameters, p => p.Direction == ParameterDirection.ReturnValue);
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

        public int ExecuteNonQuery(params IDataParameter[] parameters) => Execute(c => c.ExecuteNonQuery(), out _, parameters);

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public int ExecuteQuery(DataSet results, params IDataParameter[] parameters)
        {
            return Execute(
                command => DbCommandExtensions.FillDataSet(
                    command, 
                    Verifier.IsNotNull(Verifier.VerifyArgument(results, nameof(results))).Value, 
                    () => _provider.CreateDataAdapter(command)), 
                out _,
                parameters);
        }
        public int ExecuteQuery(DataTable results, params IDataParameter[] parameters)
        {
            return Execute(
                command => DbCommandExtensions.FillDataTable(
                    command, 
                    Verifier.IsNotNull(Verifier.VerifyArgument(results, nameof(results))).Value, 
                    () => _provider.CreateDataAdapter(command)), 
                out _,
                parameters);
        }
        #endif

        public void ExecuteReader(CommandBehavior behavior, Action<DbDataReader> readAction, params IDataParameter[] parameters)
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
                out _,
                parameters);
        }

        public object ExecuteScalar(params IDataParameter[] parameters) => Execute(command => command.ExecuteScalar(), out _, parameters);

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public int ExecuteStoredProcedure(DataSet results, params IDataParameter[] parameters)
        {
            Execute<object>(
                command => DbCommandExtensions.FillDataSet(
                    command, 
                    Verifier.IsNotNull(Verifier.VerifyArgument(results, nameof(results))), 
                    () => _provider.CreateDataAdapter(command)),
                out var returnValue,
                parameters);
            return returnValue;
        }
        public int ExecuteStoredProcedure(DataTable results, params IDataParameter[] parameters)
        {
            Execute<object>(
                command => DbCommandExtensions.FillDataTable(
                    command, 
                    Verifier.IsNotNull(Verifier.VerifyArgument(results, nameof(results))), 
                    () => _provider.CreateDataAdapter(command)), 
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