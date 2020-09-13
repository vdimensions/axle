using Axle.Security.Cryptography.Algorithms.Hash;
using Axle.Security.Cryptography.Algorithms.Hash.Hmac;
using NUnit.Framework;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash.Hmac
{
    [TestFixture]
    public sealed class HmacSha384IntegrityTest : AbstractSignedHashIntegrityTest
    {
        protected override IHashAlgorithm CreateAlgorithmInstance(byte[] key) => new HmacSha384HashAlgorithm(key);

        protected override string Value => "Hello";
        protected override string Secret => "World";

        protected override string ExpectedResultHash => "adc3831d3d4143a2094f1c740323d0bbe2d660a401b33ef5e07319e8013686a69eb3469522608ba0af91fe798dca9b97";
    }
}