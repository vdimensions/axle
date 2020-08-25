using System.Security.Cryptography;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the SHA384 hash function.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class HmacSha384HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacSha384HashAlgorithm() : base(new HMACSHA384()) { }
        public HmacSha384HashAlgorithm(byte[] key) 
            : base(new HMACSHA384(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
    }
}