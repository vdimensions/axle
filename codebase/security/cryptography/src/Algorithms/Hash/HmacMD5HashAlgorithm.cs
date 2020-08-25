using System.Security.Cryptography;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the MD5 hash function.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class HmacMD5HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacMD5HashAlgorithm() : base(new HMACMD5()) { }
        public HmacMD5HashAlgorithm(byte[] key) 
            : base(new HMACMD5(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
    }
}