using System.Security.Cryptography;

namespace Axle.Security.Cryptography.Algorithms.Symmetric
{
    [System.Obsolete("Consider using AES. The DES cipher is considered broken. ")]
    public sealed class DesHashAlgorithm : AbstractSymmetricHashAlgorithm<DESCryptoServiceProvider>
    {
        public DesHashAlgorithm() : base(new DESCryptoServiceProvider()) { }
    }
}