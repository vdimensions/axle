using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha512HashAlgorithm : AbstractHashAlgorithm
    {
        public Sha512HashAlgorithm() : base(SHA512.Create()) { }
    }
}