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
    public static class DataSourceConnectionExtensions
    {
        private static readonly BuildCommandCallback _nullBuilderFunc = (x) => x;

        [Obsolete("Use DataSource.GetCommand(...) method instead. This method will be removed in the next version of Axle.Data")]
        public static IDataSourceCommand GetCommand(this IDataSourceConnection connection, string commandText, CommandType commandType, BuildCommandCallback buildCommandCallback)
        {
            connection.VerifyArgument(nameof(connection)).IsNotNull();
            return connection.DataSource.GetCommand(commandText, commandType, buildCommandCallback);
        }

        [Obsolete("Use DataSource.GetCommand(...) method instead. This method will be removed in the next version of Axle.Data")]
        public static IDataSourceCommand GetCommand(this IDataSourceConnection connection, string commandText, CommandType commandType = CommandType.Text)
        {
            connection.VerifyArgument(nameof(connection)).IsNotNull();
            return connection.DataSource.GetCommand(commandText, commandType, _nullBuilderFunc);
        }

        [Obsolete("Use DataSource.GetScript(...) method instead. This method will be removed in the next version of Axle.Data")]
        public static IDataSourceCommand GetScript(this IDataSourceConnection connection, string bundle, string commandText, CommandType commandType, BuildCommandCallback buildCommandCallback)
        {
            connection.VerifyArgument(nameof(connection)).IsNotNull();
            return connection.DataSource.GetScript(bundle, commandText, commandType, buildCommandCallback);
        }

        [Obsolete("Use DataSource.GetScript(...) method instead. This method will be removed in the next version of Axle.Data")]
        public static IDataSourceCommand GetScript(this IDataSourceConnection connection, string bundle, string scriptPath, CommandType commandType = CommandType.Text)
        {
            connection.VerifyArgument(nameof(connection)).IsNotNull();
            return connection.DataSource.GetScript(bundle, scriptPath, commandType, _nullBuilderFunc);
        }
        [Obsolete("Use DataSource.GetScript(...) method instead. This method will be removed in the next version of Axle.Data")]
        public static IDataSourceCommand GetScript(this IDataSourceConnection connection, string scriptPath, CommandType commandType, BuildCommandCallback buildCommandFunc)
        {
            connection.VerifyArgument(nameof(connection)).IsNotNull();
            return connection.DataSource.GetScript(DataSourceModule.SqlScriptsBundle, scriptPath, commandType, buildCommandFunc);
        }
        [Obsolete("Use DataSource.GetScript(...) method instead. This method will be removed in the next version of Axle.Data")]
        public static IDataSourceCommand GetScript(this IDataSourceConnection connection, string scriptPath, CommandType commandType = CommandType.Text)
        {
            connection.VerifyArgument(nameof(connection)).IsNotNull();
            return GetScript(connection, scriptPath, commandType, _nullBuilderFunc);
        }
    }
}