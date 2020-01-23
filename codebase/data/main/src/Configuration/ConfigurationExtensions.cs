#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Axle.Configuration;

namespace Axle.Data.Configuration
{
    /// <summary>
    /// A static class to contain extension methods for configuration objects
    /// </summary>
    public static class ConfigExtensions
    {
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private sealed class ConnectionStringData
        {
            public string Name { get;set; }
            public string ProviderName { get;set; }
            public string ConnectionString { get;set; }
        }
        /// <summary>
        /// Obtains a collection of <see cref="ConnectionStringInfo"/> objects,
        /// each representing a particular connection string configuration.
        /// </summary>
        /// <param name="config">
        /// The <see cref="IConfigSection">configuration object</see> to obtain connection string information from,
        /// </param>
        /// <returns>
        /// A collection of <see cref="ConnectionStringInfo"/> objects,
        /// each representing a particular connection string configuration.
        /// </returns>
        public static IEnumerable<ConnectionStringInfo> GetConnectionStrings(this IConfigSection config)
        {
            var section = config.GetSection("ConnectionStrings");
            return section.Keys.Select(k => section.GetSection<ConnectionStringData>(k))
                .Select(x => new ConnectionStringInfo(x.Name, x.ProviderName, x.ConnectionString))
                .ToArray();
        }
    }
}
#endif
