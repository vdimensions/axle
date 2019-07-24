using Axle.Verification;

namespace Axle.Configuration
{
    public sealed class ConfigSetting : IConfigSetting
    {
        public static IConfigSetting Create(string value) => new ConfigSetting(Verifier.IsNotNull(Verifier.VerifyArgument(value, nameof(value))));

        private ConfigSetting(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}