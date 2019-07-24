#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;

namespace Axle.Configuration.Legacy.Adapters
{
    internal sealed class ConnectionStringsSectionAdapter : AbstractLegacySection
    {
        public ConnectionStringsSectionAdapter(ConnectionStringsSection section) : base(nameof(System.Configuration.Configuration.ConnectionStrings))
        {
            foreach (ConnectionStringSettings connectionString in section.ConnectionStrings)
            {
                Sections.Add(connectionString.Name, new ConnectionStringSettingAdapter(connectionString, connectionString.Name));
            }
        }
    }
}
#endif