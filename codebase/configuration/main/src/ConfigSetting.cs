using Axle.Text;
using Axle.Verification;

namespace Axle.Configuration
{
    public sealed class ConfigSetting : IConfigSetting
    {
        public static IConfigSetting Create(CharSequence value) => new ConfigSetting(
            Verifier.IsNotNull(Verifier.VerifyArgument(value, nameof(value))));

        private ConfigSetting(CharSequence value)
        {
            Value = value;
        }

        public CharSequence Value { get; }
    }
}