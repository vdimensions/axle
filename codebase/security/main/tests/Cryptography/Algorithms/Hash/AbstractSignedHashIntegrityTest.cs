using Axle.Security.Cryptography.Algorithms;
using Axle.Security.Cryptography.Algorithms.Hash;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash
{
    public abstract class AbstractSignedHashIntegrityTest : AbstractHashIntegrityTest
    {
        protected abstract IHashAlgorithm CreateAlgorithmInstance(byte[] key);
        protected override IEncryptionAlgorithm CreateAlgorithmInstance() => CreateAlgorithmInstance(Encoding.GetBytes(Secret));

        protected abstract string Secret { get; }
    }
}