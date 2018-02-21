using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the MD5 hash function.
    /// </summary>
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class HmacMD5HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacMD5HashAlgorithm() : base(new HMACMD5()) { }
    }
}