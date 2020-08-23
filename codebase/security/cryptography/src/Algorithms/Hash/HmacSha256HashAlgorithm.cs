using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the SHA256 hash function.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class HmacSha256HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacSha256HashAlgorithm() : base(new HMACSHA256()) { }
        public HmacSha256HashAlgorithm(byte[] key) 
            : base(new HMACSHA256(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
    }
}