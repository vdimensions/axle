#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;

namespace Axle.Configuration.ConfigurationManager.Adapters
{
    internal sealed class ConnectionStringsSectionAdapter : AbstractConfigurationManagerSectionAdapter
    {
        public ConnectionStringsSectionAdapter(ConnectionStringsSection section) 
            : base(nameof(System.Configuration.Configuration.ConnectionStrings))
        {
            foreach (ConnectionStringSettings connectionString in section.ConnectionStrings)
            {
                Sections.Add(
                    connectionString.Name, 
                    new ConnectionStringSettings2ConfigSectionAdapter(connectionString, connectionString.Name));
            }
        }
    }
}
#endif