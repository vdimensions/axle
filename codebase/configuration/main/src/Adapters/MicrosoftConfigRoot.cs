#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;
using Microsoft.Extensions.Configuration;

namespace Axle.Configuration.Adapters
{
    using IMSConfigurationRoot = Microsoft.Extensions.Configuration.IConfigurationRoot;
    using IMSConfigurationSource = Microsoft.Extensions.Configuration.IConfigurationSource;

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

            var b = new ConfigurationBuilder();
            b.Sources.Add(source);
            return b.Build();
        }

        public MicrosoftConfigRoot(IMSConfigurationRoot configuration) : base(configuration) { }
        public override string Value => null;
        public override string Name => string.Empty;
    }
}
#endif