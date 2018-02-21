using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha512NativeHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha512NativeHashAlgorithm() : base(new SHA512CryptoServiceProvider()) { }
    }
}