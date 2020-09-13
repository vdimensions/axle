using System.Security;
using System.Security.Cryptography;
using System.Text;
using Axle.Security.Extensions.SecureString;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash.Hmac
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the SHA256 hash function.
    /// </summary>
    public sealed class HmacSha256HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacSha256HashAlgorithm() : base(new HMACSHA256()) { }
        public HmacSha256HashAlgorithm(byte[] key) 
            : base(new HMACSHA256(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
        public HmacSha256HashAlgorithm(SecureString key) 
            : this(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value.Map(Encoding.UTF8.GetBytes)) { }
    }
}