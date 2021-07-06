using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Symmetric
{
    [System.Obsolete("Prefer AES instead. The RC2 cipher is considered broken.")]
    public sealed class RC2HashAlgorithm : AbstractSymmetricHashAlgorithm<RC2CryptoServiceProvider>
    {
        public RC2HashAlgorithm() : base(new RC2CryptoServiceProvider()) { }
    }
}