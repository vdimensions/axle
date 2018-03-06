using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Symmetric.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Symmetric
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class RC2HashAlgorithm : AbstractSymmetricHashAlgorithm
    {
        public RC2HashAlgorithm() : base(new RC2CryptoServiceProvider()) { }
    }
}