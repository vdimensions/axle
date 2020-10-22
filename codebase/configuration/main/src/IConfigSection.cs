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
        
        /// <summary>
        /// Gets the name of the current <see cref="IConfigSection">configuration section</see>.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets a collection of the keys for the different <see cref="IConfigSetting">configuration settings</see>
        /// contained within the current <see cref="IConfigSection">configuration section</see>.
        /// </summary>
        IEnumerable<string> Keys { get; }
    }
}