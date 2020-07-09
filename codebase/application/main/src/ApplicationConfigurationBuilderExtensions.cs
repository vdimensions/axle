using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle
{
    /// <summary>
    /// A static class containing extension methods for instances of the <see cref="IApplicationConfigurationBuilder"/>
    /// interface.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class ApplicationConfigurationBuilderExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static IApplicationConfigurationBuilder EnableLegacyConfig(this IApplicationConfigurationBuilder builder)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(builder, nameof(builder)));
            return builder.Add(new Axle.Configuration.Legacy.LegacyConfigSource());
        }
        #endif
    }
}