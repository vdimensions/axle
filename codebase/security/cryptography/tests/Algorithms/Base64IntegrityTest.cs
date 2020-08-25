using System.Text;
using Axle.Security.Cryptography.Algorithms;
using NUnit.Framework;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash
{
    [TestFixture]
    public sealed class Base64IntegrityTest
    {
        protected IEncryptionAlgorithm CreateAlgorithmInstance() => new Base64EncodeAlgorithm();
        
        [Test]
        public void TestHashedStringIntegrity()
        {
            var calculatedHash = CreateAlgorithmInstance().Encrypt(Value, Encoding);
            Assert.AreEqual(ExpectedResultHash, calculatedHash);
        }
        
        protected Encoding Encoding  => Encoding.ASCII;
        protected string Value => "Hello World";
        protected string ExpectedResultHash => "SGVsbG8gV29ybGQ=";
    }
}