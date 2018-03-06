using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha512ManagedHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha512ManagedHashAlgorithm() : base(new SHA512Managed()) { }
    }
}