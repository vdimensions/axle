#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using Axle.Configuration.Microsoft.Adapters;
using Axle.Verification;

namespace Axle.Configuration.Microsoft
{
    using MSFileConfigurationSource = global::Microsoft.Extensions.Configuration.FileConfigurationSource;

    /// <summary>
    /// A wrapper around the <see cref="MSFileConfigurationSource"/> class. Allows usages of file-based configuration
    /// formats that are supported by the Microsoft Extensions library to be used as a <see cref="IConfigSource"/>. 
    /// </summary>
    /// <seealso cref="MSFileConfigurationSource"/>
    public sealed class FileConfigSource : IConfigSource
    {
        private readonly MSFileConfigurationSource _source;

        /// <summary>
        /// Creates a new instance of the <see cref="FileConfigSource"/> class using the provided
        /// <see cref="MSFileConfigurationSource"/> <paramref name="origin"/> as the underlying configuration source
        /// object.
        /// </summary>
        /// <param name="origin">
        /// A <see cref="MSFileConfigurationSource"/> instance.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="origin"/> is <c>null</c>.
        /// </exception>
        public FileConfigSource(MSFileConfigurationSource origin)
        {
            _source = Verifier.IsNotNull(Verifier.VerifyArgument(origin, nameof(origin)));
        }

        /// <inheritdoc/>
        public IConfiguration LoadConfiguration()
        {
            return Microsoft2AxleConfigRootAdapter.FromConfigurationSource(_source);
        }
    }
}
#endif