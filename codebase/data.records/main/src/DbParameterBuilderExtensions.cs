using System.Collections.Generic;
using System.Data;
using Axle.Verification;

namespace Axle.Data.Records
{
    /// <summary>
    /// A static class to contain extension methods relevant for <see cref="IDbParameterBuilder"/> implementations.
    /// </summary>
    public static class DbParameterBuilderExtensions
    {
        /// <summary>
        /// Creates a collection of input <see cref="IDataParameter">database parameters</see> based on the
        /// field values of the provided <paramref name="record"/>.
        /// </summary>
        /// <param name="parameterBuilder">
        /// The <see cref="IDbParameterBuilder"/> instance that will create the parameter objects.
        /// </param>
        /// <param name="record">
        /// The <see cref="DataRecord">record</see> to parametrize.
        /// </param>
        /// <returns>
        /// A collection of input <see cref="IDataParameter">database parameters</see> based on the
        /// field values of the provided <see cref="DataRecord">record</see> instance.
        /// </returns>
        public static IEnumerable<IDataParameter> Parametrize(this IDbParameterBuilder parameterBuilder, DataRecord record)
        {
            parameterBuilder.VerifyArgument(nameof(parameterBuilder)).IsNotNull();
            record.VerifyArgument(nameof(record)).IsNotNull();
            for (var i = 0; i < record.FieldCount; ++i)
            {
                var fieldName = record.GetName(i);
                var fieldValue = record[i];
                var param = parameterBuilder.CreateParameter(fieldName, ParameterDirection.Input)
                    .SetValue(fieldValue)
                    .Build();
                yield return param;
            }
        }
    }
}