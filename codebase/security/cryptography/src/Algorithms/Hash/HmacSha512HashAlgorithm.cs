using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the SHA512 hash function.
    /// </summary>
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class HmacSha512HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacSha512HashAlgorithm() : base(new HMACSHA512()) { }
    }
}