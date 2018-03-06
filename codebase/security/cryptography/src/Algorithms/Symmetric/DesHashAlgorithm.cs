using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Symmetric.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Symmetric
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class DesHashAlgorithm : AbstractSymmetricHashAlgorithm
    {
        public DesHashAlgorithm() : base(new DESCryptoServiceProvider()) { }
    }
}