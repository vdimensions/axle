#if NETFRAMEWORK
using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class MacTripleDesHashAlgorithm : AbstractHashAlgorithm
    {
        public MacTripleDesHashAlgorithm() : base(new MACTripleDES()) { }
        public MacTripleDesHashAlgorithm(byte[] key) 
            : base(new MACTripleDES(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
    }
}
#endif