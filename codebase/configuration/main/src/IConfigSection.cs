using System.Collections.Generic;

namespace Axle.Configuration
{
    /// <summary>
    /// An interface representing a configuration section object.
    /// </summary>
    public interface IConfigSection : IConfigSetting
    {
        /// <summary>
        /// Gets a configuration <see cref="IConfigSetting">setting</see>.
        /// </summary>
        /// <param name="key">
        /// The configuration key.
        /// </param>
        /// <returns>
        /// A <see cref="IConfigSetting"/> instance representing the retrieved configuration value.
        /// </returns>
        IEnumerable<IConfigSetting> this[string key] { get; }
        string Name { get; }
        IEnumerable<string> Keys { get; }
    }
}