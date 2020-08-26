using Axle.Security.Cryptography.Algorithms;
using Axle.Security.Cryptography.Algorithms.Hash;
using NUnit.Framework;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash
{
    [TestFixture]
    public sealed class MD5NativeIntegrityTest : AbstractMD5IntegrityTest
    {
        protected override IEncryptionAlgorithm CreateAlgorithmInstance() => new MD5NativeHashAlgorithm();
    }
}