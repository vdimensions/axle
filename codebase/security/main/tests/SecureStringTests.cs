using System;
using System.Linq;
using System.Security;
using Axle.Security.Extensions.SecureString;
using NUnit.Framework;

namespace Axle.Security.Tests
{
    [TestFixture]
    public class SecureStringTests
    {
        [Test]
        public void TestSecureStringAllocDeAlloc()
        {
            const string RAW_STRING = "HELLO WORLD";
            var sstr = new SecureString();
            RAW_STRING.ToList().ForEach(sstr.AppendChar);
            sstr.MakeReadOnly();
            
            sstr.With(str => Assert.AreEqual(RAW_STRING, str));
        }
        
        [Test]
        public void TestSecureStringAllocDeAllocException()
        {
            const string RAW_STRING = "HELLO WORLD";
            var sstr = new SecureString();
            RAW_STRING.ToList().ForEach(sstr.AppendChar);
            sstr.MakeReadOnly();

            Assert.Throws<InvalidOperationException>(() =>
            {
                sstr.With(str => throw new InvalidOperationException());
            });
        }
    }
}