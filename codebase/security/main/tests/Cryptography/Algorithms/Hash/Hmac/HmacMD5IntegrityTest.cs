using Axle.Security.Cryptography.Algorithms.Hash;
using Axle.Security.Cryptography.Algorithms.Hash.Hmac;
using NUnit.Framework;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash.Hmac
{
    [TestFixture]
    public sealed class HmacMD5IntegrityTest : AbstractSignedHashIntegrityTest
    {
        protected override IHashAlgorithm CreateAlgorithmInstance(byte[] key) => new HmacMD5HashAlgorithm(key);

        protected override string Value => "Hello";
        protected override string Secret => "World";

        protected override string ExpectedResultHash => "d626e82f996dcddeb8f0abf68ea14e82";
    }
}