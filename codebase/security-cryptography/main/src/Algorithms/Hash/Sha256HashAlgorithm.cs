using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha256HashAlgorithm : AbstractHashAlgorithm
    {
        public Sha256HashAlgorithm() : base(SHA256.Create()) { }
    }
}