using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public sealed class Sha256NativeHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha256NativeHashAlgorithm() : base(new SHA256CryptoServiceProvider()) { }
    }
}