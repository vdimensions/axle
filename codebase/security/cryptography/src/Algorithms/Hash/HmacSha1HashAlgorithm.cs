using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the SHA1 hash function.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class HmacSha1HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacSha1HashAlgorithm() : base(new HMACSHA1()) { }
        public HmacSha1HashAlgorithm(byte[] key) 
            : base(new HMACSHA1(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
    }
}