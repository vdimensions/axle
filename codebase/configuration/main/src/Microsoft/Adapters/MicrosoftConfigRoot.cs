#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Configuration.Microsoft.Adapters
{
    using IMSConfigurationRoot = global::Microsoft.Extensions.Configuration.IConfigurationRoot;
    using IMSConfigurationSource = global::Microsoft.Extensions.Configuration.IConfigurationSource;
    using MSConfigurationBuilder = global::Microsoft.Extensions.Configuration.ConfigurationBuilder;

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class MicrosoftConfigRoot : MicrosoftConfigAdapter<IMSConfigurationRoot>, IConfiguration
    {
        public static MicrosoftConfigRoot FromSource(IMSConfigurationSource source)
        {
            return new MicrosoftConfigRoot(GetConfiguration(source));
        }

        internal static IMSConfigurationRoot GetConfiguration(IMSConfigurationSource source)
        {
            source.VerifyArgument(nameof(source)).IsNotNull();

            var b = new MSConfigurationBuilder();
            b.Sources.Add(source);
            return b.Build();
        }

        public MicrosoftConfigRoot(IMSConfigurationRoot configuration) : base(configuration) { }

        /// <inheritdoc />
        public override string Value => null;

        /// <inheritdoc />
        public override string Name => string.Empty;
    }
}
#endif