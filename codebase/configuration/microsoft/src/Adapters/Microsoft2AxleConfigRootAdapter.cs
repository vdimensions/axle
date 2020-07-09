#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Configuration.Microsoft.Adapters
{
    using IMSConfigurationRoot = global::Microsoft.Extensions.Configuration.IConfigurationRoot;
    using IMSConfigurationSource = global::Microsoft.Extensions.Configuration.IConfigurationSource;
    using MSConfigurationBuilder = global::Microsoft.Extensions.Configuration.ConfigurationBuilder;

    /// <summary>
    /// An adapter class that bridges the functionality of an instance of <see cref="IMSConfigurationRoot"/> to
    /// trough its Axle analogue -- the <see cref="IConfiguration"/> interface.
    /// </summary>
    /// <seealso cref="Microsoft2AxleConfigAdapter{T}"/>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class Microsoft2AxleConfigRootAdapter : Microsoft2AxleConfigAdapter<IMSConfigurationRoot>, IConfiguration
    {
        /// <summary>
        /// Creates a new <see cref="Microsoft2AxleConfigRootAdapter"/> instance from the provided
        /// <paramref name="configurationSource"/>.
        /// </summary>
        /// <param name="configurationSource">
        /// A <see cref="IMSConfigurationSource"/> instance used to load the <see cref="IMSConfigurationRoot"/>. 
        /// </param>
        /// <returns>
        /// An instance of <see cref="Microsoft2AxleConfigRootAdapter"/> built from the provided
        /// <paramref name="configurationSource"/>.
        /// </returns>
        public static Microsoft2AxleConfigRootAdapter FromConfigurationSource(IMSConfigurationSource configurationSource)
        {
            return new Microsoft2AxleConfigRootAdapter(GetConfiguration(configurationSource));
        }

        internal static IMSConfigurationRoot GetConfiguration(IMSConfigurationSource source)
        {
            source.VerifyArgument(nameof(source)).IsNotNull();

            var b = new MSConfigurationBuilder();
            b.Sources.Add(source);
            return b.Build();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Microsoft2AxleConfigRootAdapter"/> class using the provided
        /// <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">
        /// The <see cref="IMSConfigurationRoot"/> instance to adapt.
        /// </param>
        public Microsoft2AxleConfigRootAdapter(IMSConfigurationRoot configuration) : base(configuration) { }

        /// <inheritdoc />
        public override string Value => null;

        /// <inheritdoc />
        public override string Name => string.Empty;
    }
}
#endif