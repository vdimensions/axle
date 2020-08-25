using Axle.Security.Cryptography.Algorithms.Hash;
using NUnit.Framework;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash
{
    [TestFixture]
    public sealed class HmacSha512IntegrityTest : AbstractSignedHashIntegrityTest
    {
        protected override IHashAlgorithm CreateAlgorithmInstance(byte[] key) => new HmacSha512HashAlgorithm(key);

        protected override string Value => "Hello";
        protected override string Secret => "World";

        protected override string ExpectedResultHash => "0511fdb5129d28d374c0d44363273adb26c9f1ccc76797d671aca064fb9961e6cdfb03a51e7498c31265642f1ed39c0111d32e23e7572923405102c982036af1";
    }
}