#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
namespace Axle.Configuration.Microsoft.Adapters
{
    using IMSConfigurationSection = global::Microsoft.Extensions.Configuration.IConfigurationSection;
    
    public sealed class MicrosoftConfigSectionAdapter : MicrosoftConfigAdapter<IMSConfigurationSection>
    {
        public MicrosoftConfigSectionAdapter(IMSConfigurationSection configuration) : base(configuration) { }

        /// <inheritdoc />
        public override string Value => UnderlyingConfiguration.Value;

        /// <inheritdoc />
        public override string Name => UnderlyingConfiguration.Key;
    }
}
#endif