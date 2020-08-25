using System.Text;
using Axle.Security.Cryptography.Algorithms;
using Axle.Security.Cryptography.Algorithms.Hash;
using NUnit.Framework;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash
{
    public abstract class AbstractHashIntegrityTest
    {
        protected abstract IEncryptionAlgorithm CreateAlgorithmInstance();

        [Test]
        public void TestHashedStringIntegrity()
        {
            var calculatedHash = CreateAlgorithmInstance().Encrypt(Value, Encoding).ToLower();
            Assert.AreEqual(ExpectedResultHash, calculatedHash);
        }
        [Test]
        public void TestHashedBytesIntegrity()
        {
            var calculatedBytes = CreateAlgorithmInstance().Encrypt(Encoding.GetBytes(Value));
            var expectedBytes = new HexConverter().ConvertBack(ExpectedResultHash);
            Assert.AreEqual(expectedBytes, calculatedBytes);
        }

        protected virtual Encoding Encoding { get; } = Encoding.UTF8;
        protected abstract string ExpectedResultHash { get; }
        protected abstract string Value { get; }
    }
}