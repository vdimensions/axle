using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Symmetric
{
    /// <summary>
    /// Defines a wrapper object to access the cryptographic service provider (CSP) version of the TripleDES algorithm.
    /// This class cannot be inherited.
    /// </summary>
    /// <seealso cref="TripleDESCryptoServiceProvider"/>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class TripleDesHashAlgorithm : AbstractSymmetricHashAlgorithm
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TripleDesHashAlgorithm" /> class.
        /// </summary>
        public TripleDesHashAlgorithm() : base(new TripleDESCryptoServiceProvider()) { }
    }
}
