using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Data.DataSources
{
    /// <summary>
    /// A static class to contain extension methods for <see cref="IDataSourceConnection"/> instances.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class DataSourceExtensions
    {
        private static readonly BuildCommandCallback _nullBuilderFunc = (cb) => cb;

        public static IDataSourceCommand GetCommand(this IDataSource dataSource, string commandText, CommandType commandType = CommandType.Text)
        {
            dataSource.VerifyArgument(nameof(dataSource)).IsNotNull();
            return dataSource.GetCommand(commandText, commandType, _nullBuilderFunc);
        }

        public static IDataSourceCommand GetScript(this IDataSource dataSource, string bundle, string scriptPath, CommandType commandType = CommandType.Text)
        {
            dataSource.VerifyArgument(nameof(dataSource)).IsNotNull();
            return dataSource.GetScript(bundle, scriptPath, commandType, _nullBuilderFunc);
        }
        public static IDataSourceCommand GetScript(this IDataSource dataSource, string scriptPath, CommandType commandType, BuildCommandCallback buildCommandFunc)
        {
            dataSource.VerifyArgument(nameof(dataSource)).IsNotNull();
            return dataSource.GetScript(DataSourceModule.SqlScriptsBundle, scriptPath, commandType, buildCommandFunc);
        }
        public static IDataSourceCommand GetScript(this IDataSource dataSource, string scriptPath, CommandType commandType = CommandType.Text)
        {
            dataSource.VerifyArgument(nameof(dataSource)).IsNotNull();
            return GetScript(dataSource, scriptPath, commandType, _nullBuilderFunc);
        }
    }
}