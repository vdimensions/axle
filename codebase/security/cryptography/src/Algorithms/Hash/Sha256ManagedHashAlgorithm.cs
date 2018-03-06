using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha256ManagedHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha256ManagedHashAlgorithm() : base(new SHA256Managed()) { }
    }
}