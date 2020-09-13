#if NETFRAMEWORK
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Axle.Security.Extensions.SecureString;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash.Hmac
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the RIPEMD160 hash function.
    /// </summary>
    public sealed class HmacRIPEMD160HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacRIPEMD160HashAlgorithm() : base(new HMACRIPEMD160()) { }
        public HmacRIPEMD160HashAlgorithm(byte[] key) 
            : base(new HMACRIPEMD160(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
        public HmacRIPEMD160HashAlgorithm(SecureString key) 
            : this(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value.Map(Encoding.UTF8.GetBytes)) { }
    }
}
#endif