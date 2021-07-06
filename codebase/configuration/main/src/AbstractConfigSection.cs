using System;
using System.Collections.Generic;
using System.Linq;
using Axle.Text;
using Axle.Verification;

namespace Axle.Configuration
{
    /// <summary>
    /// An abstract class to serve as a base for implementing the <see cref="IConfigSection"/> interface.
    /// </summary>
    public abstract class AbstractConfigSection : IConfigSection
    {
        /// <summary>
        /// A dictionary representing the current <see cref="AbstractConfigSection"/> implementation's logical
        /// <see cref="IConfigSetting">configuration settings</see> children. 
        /// </summary>
        protected IDictionary<string, IList<IConfigSetting>> Data { get; }

        /// <summary>
        /// Initializes the <see cref="AbstractConfigSection"/> class using the specified setting
        /// <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the configuration setting represented by the current <see cref="AbstractConfigSection"/>
        /// implementation.
        /// </param>
        protected AbstractConfigSection(string name)
        {
            Name = name;
            Data = new Dictionary<string, IList<IConfigSetting>>(StringComparer.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        public IEnumerable<string> Keys => Data.Keys;

        /// <inheritdoc />
        public string Name { get; }

        CharSequence IConfigSetting.Value => null;

        /// <inheritdoc />
        public IEnumerable<IConfigSetting> this[string key]
        {
            get
            {
                StringVerifier.IsNotNullOrEmpty(Verifier.VerifyArgument(key, nameof(key)));
                return Data.TryGetValue(key, out var val) ? val : Enumerable.Empty<IConfigSetting>();
            }
        }
    }
}