using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha384NativeHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha384NativeHashAlgorithm() : base(new SHA384CryptoServiceProvider()) { }
    }
}