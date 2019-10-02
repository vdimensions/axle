using System.Data;

namespace Axle.Data.DataSources
{
    public interface IDataSource : IDbParameterBuilder
    {
        IDataSourceConnection OpenConnection();

        IDataSourceCommand GetCommand(string commandText, CommandType commandType, BuildCommandCallback buildCommandCallback);

        IDataSourceCommand GetScript(string bundle, string scriptPath, CommandType commandType, BuildCommandCallback buildCommandCallback);

        string DialectName { get; }
        string ConnectionString { get; }
    }
}