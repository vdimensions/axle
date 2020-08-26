#if NETFRAMEWORK
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Axle.Security.Extensions.SecureString;
using Axle.Verification;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the RIPEMD160 hash function.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class HmacRipeMD160HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacRipeMD160HashAlgorithm() : base(new HMACRIPEMD160()) { }
        public HmacRipeMD160HashAlgorithm(byte[] key) 
            : base(new HMACRIPEMD160(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
        public HmacRipeMD160HashAlgorithm(SecureString key) 
            : this(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value.Map(Encoding.UTF8.GetBytes)) { }
    }
}
#endif