using System.Security;
using System.Security.Cryptography;
using System.Text;
using Axle.Security.Extensions.SecureString;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash.Hmac
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the MD5 hash function.
    /// </summary>
    public sealed class HmacMD5HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacMD5HashAlgorithm() : base(new HMACMD5()) { }
        public HmacMD5HashAlgorithm(byte[] key) 
            : base(new HMACMD5(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
        public HmacMD5HashAlgorithm(SecureString key) 
            : this(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value.Map(Encoding.UTF8.GetBytes)) { }
    }
}