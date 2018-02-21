using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha1HashAlgorithm : AbstractHashAlgorithm
    {
        public Sha1HashAlgorithm() : base(SHA1.Create()) { }
    }
}