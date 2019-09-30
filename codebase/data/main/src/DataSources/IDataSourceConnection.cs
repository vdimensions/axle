using System;
using System.Data;

namespace Axle.Data.DataSources
{
    public interface IDataSourceConnection : IDataSourceResource<IDbConnection>, IDisposable
    {
        IDataSourceTransaction BeginTransaction(IsolationLevel isolationLevel);

        IDataSourceCommand GetCommand(string commandText, CommandType commandType, Func<ICommandBuilder, ICommandBuilder> buildCommandCallback);

        IDataSourceCommand GetScript(string bundle, string scriptPath, CommandType commandType, Func<ICommandBuilder, ICommandBuilder> buildCommandCallback);

        IDataSourceTransaction CurrentTransaction { get; }
    }
}