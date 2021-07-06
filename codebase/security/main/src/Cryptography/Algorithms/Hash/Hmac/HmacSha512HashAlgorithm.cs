using System.Security;
using System.Security.Cryptography;
using System.Text;
using Axle.Security.Extensions.SecureString;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash.Hmac
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the SHA512 hash function.
    /// </summary>
    public sealed class HmacSha512HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacSha512HashAlgorithm() : base(new HMACSHA512()) { }
        public HmacSha512HashAlgorithm(byte[] key) 
            : base(new HMACSHA512(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
        public HmacSha512HashAlgorithm(SecureString key) 
            : this(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value.Map(Encoding.UTF8.GetBytes)) { }
    }
}