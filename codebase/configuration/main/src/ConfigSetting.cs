using Axle.Text;
using Axle.Verification;

namespace Axle.Configuration
{
    /// <summary>
    /// The default implementation class for the <see cref="IConfigSetting"/> interface.
    /// </summary>
    public sealed class ConfigSetting : IConfigSetting
    {
        /// <summary>
        /// Creates a new <see cref="IConfigSetting"/> instance from the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// A <see cref="CharSequence"/> representing the value of the configuration setting.
        /// </param>
        /// <returns>
        /// A new <see cref="IConfigSetting"/> instance from the provided <paramref name="value"/>.
        /// </returns>
        public static IConfigSetting Create(CharSequence value) 
            => new ConfigSetting(Verifier.IsNotNull(Verifier.VerifyArgument(value, nameof(value))));

        private ConfigSetting(CharSequence value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public CharSequence Value { get; }
    }
}