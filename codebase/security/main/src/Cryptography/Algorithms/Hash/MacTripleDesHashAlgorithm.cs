#if NETFRAMEWORK
using System.Security.Cryptography;
using Axle.Verification;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    public sealed class MacTripleDesHashAlgorithm : AbstractHashAlgorithm
    {
        public MacTripleDesHashAlgorithm() : base(new MACTripleDES()) { }
        public MacTripleDesHashAlgorithm(byte[] key) 
            : base(new MACTripleDES(Verifier.IsNotNull(Verifier.VerifyArgument(key, nameof(key))).Value)) { }
    }
}
#endif