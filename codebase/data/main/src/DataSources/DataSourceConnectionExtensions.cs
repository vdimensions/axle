﻿using System;
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
        private static readonly Func<ICommandBuilder, ICommandBuilder> _nullBuilderFunc = x => x;

        public static IDataSourceCommand GetCommand(this IDataSourceConnection connection, string commandText, CommandType commandType = CommandType.Text)
        {
            connection.VerifyArgument(nameof(connection)).IsNotNull();
            return connection.GetCommand(commandText, commandType, _nullBuilderFunc);
        }

        public static IDataSourceCommand GetScript(this IDataSourceConnection connection, string scriptPath, CommandType commandType = CommandType.Text)
        {
            connection.VerifyArgument(nameof(connection)).IsNotNull();
            return connection.GetScript(scriptPath, commandType, _nullBuilderFunc);
        }
    }
}