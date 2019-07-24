using System;
using Axle.Extensions.Object;

namespace Axle.Data.Configuration
{
    /// <summary>
    /// A class representing the connection string information, Can be used to bind connection string configuration data.
    /// </summary>
    public sealed class ConnectionStringInfo : IEquatable<ConnectionStringInfo>
    {
        internal ConnectionStringInfo(string name, string providerName, string connectionString)
        {
            Name = name;
            ProviderName = providerName;
            ConnectionString = connectionString;
        }

        internal const string ConfigSectionName = "ConnectionStrings";

        public override int GetHashCode() => this.CalculateHashCode(Name, ProviderName, ConnectionString);
        public override bool Equals(object obj) => obj is ConnectionStringInfo csi && Equals(csi);
        public bool Equals(ConnectionStringInfo other)
        {
            var cmp = StringComparer.OrdinalIgnoreCase;
            return cmp.Equals(Name, other.Name) && 
                   cmp.Equals(ProviderName, other.ProviderName) &&
                   cmp.Equals(ConnectionString, other.ConnectionString);
        }

        public string Name { get; }
        public string ProviderName { get; }
        public string ConnectionString { get; }
    }
}
