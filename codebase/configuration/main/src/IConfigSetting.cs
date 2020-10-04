using Axle.Text;

namespace Axle.Configuration
{
    /// <summary>
    /// An interface representing a configuration setting,
    /// </summary>
    public interface IConfigSetting
    {
        /// <summary>
        /// Gets a <see cref="CharSequence"/> representing the value of the current <see cref="IConfigSetting"/>.
        /// </summary>
        CharSequence Value { get; }
    }
}