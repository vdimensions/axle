using Axle.Security.Cryptography.Algorithms.Hash;
using Axle.Security.Cryptography.Algorithms.Hash.Hmac;
using NUnit.Framework;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash.Hmac
{
    [TestFixture]
    public sealed class HmacSha256IntegrityTest : AbstractSignedHashIntegrityTest
    {
        protected override IHashAlgorithm CreateAlgorithmInstance(byte[] key) => new HmacSha256HashAlgorithm(key);

        protected override string Value => "Hello";
        protected override string Secret => "World";

        protected override string ExpectedResultHash => "46288437613044114d21e7fad79837c12336202f4c85008548fb226693426f56";
    }
}