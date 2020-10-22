using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Data
{
    /// <summary>
    /// A static class to provide common extension methods to implementations of the <see cref="IDbParameterBuilder"/>
    /// interface.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class DbParameterBuilderExtensions
    {
        public static IDataParameter CreateInputParameter(this IDbParameterBuilder builder, string name, Func<IDbParameterValueBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            Verifier.IsNotNull(Verifier.VerifyArgument(buildFunc, nameof(buildFunc)));
            var p = buildFunc(builder.CreateParameter(name, ParameterDirection.Input));
            return p.Build();
        }

        public static IDataParameter CreateInputOutputParameter(this IDbParameterBuilder builder, string name, Func<IDbParameterValueBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            Verifier.IsNotNull(Verifier.VerifyArgument(buildFunc, nameof(buildFunc)));
            var p = buildFunc(builder.CreateParameter(name, ParameterDirection.InputOutput));
            return p.Build();
        }

        public static IDataParameter CreateOutputParameter(this IDbParameterBuilder builder, string name, Func<IDbParameterValueBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            Verifier.IsNotNull(Verifier.VerifyArgument(buildFunc, nameof(buildFunc)));
            var p = buildFunc(builder.CreateParameter(name, ParameterDirection.Output));
            return p.Build();
        }

        public static IDataParameter CreateReturnParameter(this IDbParameterBuilder builder, string name, Func<IDbParameterValueBuilder, IDbParameterOptionalPropertiesBuilder> buildFunc)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            Verifier.IsNotNull(Verifier.VerifyArgument(buildFunc, nameof(buildFunc)));
            var p = buildFunc(builder.CreateParameter(name, ParameterDirection.ReturnValue));
            return p.Build();
        }
        
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Creates a collection of input <see cref="IDataParameter">database parameters</see> based on the
        /// field values of the provided <see cref="System.Data.DataRow">record</see> instance.
        /// </summary>
        /// <param name="parameterBuilder">
        /// The <see cref="IDbServiceProvider"/> instance that will create the parameter objects.
        /// </param>
        /// <param name="record">
        /// The <see cref="System.Data.DataRow">record</see> to parametrize.
        /// </param>
        /// <returns>
        /// A collection of input <see cref="IDbDataParameter">database parameters</see> based on the
        /// field values of the provided <see cref="IDataRecord">record</see> instance.
        /// </returns>
        public static IEnumerable<IDataParameter> Parametrize(this IDbParameterBuilder parameterBuilder, DataRow record)
        {
            parameterBuilder.VerifyArgument(nameof(parameterBuilder)).IsNotNull();
            record.VerifyArgument(nameof(record)).IsNotNull();
            for (var i = 0; i < record.Table.Columns.Count; ++i)
            {
                var column = record.Table.Columns[i];
                var fieldValue = record[i];
                var param = parameterBuilder.CreateParameter(column.ColumnName, ParameterDirection.Input)
                    .SetValue(fieldValue)
                    .Build();
                yield return param;
            }
        }
        #endif
    }
}