using Axle.Security.Cryptography.Algorithms.Hash;
using NUnit.Framework;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash
{
    [TestFixture]
    public sealed class HmacSha1IntegrityTest : AbstractSignedHashIntegrityTest
    {
        protected override IHashAlgorithm CreateAlgorithmInstance(byte[] key) => new HmacSha1HashAlgorithm(key);

        protected override string Value => "Hello";
        protected override string Secret => "World";

        protected override string ExpectedResultHash => "8d1a4c29af178df51b9282eaf6b8898b800e9ec5";
    }
}