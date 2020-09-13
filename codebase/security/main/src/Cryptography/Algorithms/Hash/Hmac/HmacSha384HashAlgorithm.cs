using System.Security;
using System.Security.Cryptography;
using System.Text;
using Axle.Security.Extensions.SecureString;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash.Hmac
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the SHA384 hash function.
    /// </summary>
    public sealed class HmacSha384HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacSha384HashAlgorithm() : base(new HMACSHA384()) { }
        public HmacSha384HashAlgorithm(byte[] key) 
            : base(new HMACSHA384(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
        public HmacSha384HashAlgorithm(SecureString key) 
            : this(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value.Map(Encoding.UTF8.GetBytes)) { }
    }
}