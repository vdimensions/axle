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