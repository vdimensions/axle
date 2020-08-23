using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the SHA512 hash function.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class HmacSha512HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacSha512HashAlgorithm() : base(new HMACSHA512()) { }
        public HmacSha512HashAlgorithm(byte[] key) 
            : base(new HMACSHA512(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
    }
}