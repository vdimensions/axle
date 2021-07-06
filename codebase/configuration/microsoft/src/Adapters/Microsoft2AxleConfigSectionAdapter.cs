#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using Axle.Text;

namespace Axle.Configuration.Microsoft.Adapters
{
    using IMSConfigurationSection = global::Microsoft.Extensions.Configuration.IConfigurationSection;
    
    /// <summary>
    /// An adapter class that bridges the functionality of an instance of <see cref="IMSConfigurationSection"/> to
    /// trough its Axle analogue -- the <see cref="IConfigSection"/> interface.
    /// </summary>
    /// <seealso cref="Microsoft2AxleConfigAdapter{T}"/>
    public sealed class Microsoft2AxleConfigSectionAdapter : Microsoft2AxleConfigAdapter<IMSConfigurationSection>
    {
        public Microsoft2AxleConfigSectionAdapter(IMSConfigurationSection configuration) : base(configuration) { }

        /// <inheritdoc />
        public override CharSequence Value => UnderlyingConfiguration.Value;

        /// <inheritdoc />
        public override string Name => UnderlyingConfiguration.Key;
    }
}
#endif