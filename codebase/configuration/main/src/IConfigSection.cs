using System.Collections.Generic;

namespace Axle.Configuration
{
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
        IConfigSetting this[string key] { get; }
        string Name { get; }
        IEnumerable<string> Keys { get; }
    }
}