using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Data
{
    /// <summary>
    /// A static class to provide extension methods for <see cref="IDbServiceProvider"/> implementations.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class DbServiceProviderExtensions
    {
        /// <summary>
        /// Creates a collection of input <see cref="IDbDataParameter">database parameters</see> based on the
        /// field values of the provided <see cref="IDbRecord">record</see> instance.
        /// </summary>
        /// <param name="dbServiceProvider">
        /// The <see cref="IDbServiceProvider"/> instance that will create the parameter objects.
        /// </param>
        /// <param name="record">
        /// The <see cref="IDbRecord">record</see> to parametrize.
        /// </param>
        /// <returns>
        /// A collection of input <see cref="IDbDataParameter">database parameters</see> based on the
        /// field values of the provided <see cref="IDbRecord">record</see> instance.
        /// </returns>
        public static IEnumerable<IDbDataParameter> Parametrize(this IDbServiceProvider dbServiceProvider, IDbRecord record)
        {
            dbServiceProvider.VerifyArgument(nameof(dbServiceProvider)).IsNotNull();
            record.VerifyArgument(nameof(record)).IsNotNull();
            for (var i = 0; i < record.FieldCount; ++i)
            {
                var fieldName = record.GetFieldName(i);
                var fieldValue = record[i];
                var param = dbServiceProvider.CreateParameter(fieldName, null, null, ParameterDirection.Input, fieldValue);
                yield return param;
            }
        }

        /// <summary>
        /// Gets an instance of <see cref="IDbParameterBuilder"/> object tailored for the current
        /// <see cref="IDbServiceProvider"/> instance. 
        /// </summary>
        /// <param name="dbServiceProvider">
        /// The <see cref="IDbServiceProvider"/> instance to provide the command parameter creation functionality.
        /// </param>
        /// <returns>
        /// An <see cref="IDbParameterBuilder"/> instance.
        /// </returns>
        public static IDbParameterBuilder GetDbParameterBuilder(this IDbServiceProvider dbServiceProvider)
        {
            return new DbParameterBuilder(dbServiceProvider.VerifyArgument(nameof(dbServiceProvider)).IsNotNull().Value);
        }
    }
}