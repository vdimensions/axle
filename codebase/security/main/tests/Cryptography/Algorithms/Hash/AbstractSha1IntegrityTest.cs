using NUnit.Framework;

namespace Axle.Security.Cryptography.Tests.Algorithms.Hash
{
    [TestFixture]
    public abstract class AbstractSha1IntegrityTest : AbstractHashIntegrityTest
    {
        protected override string Value => "Hello World";

        protected override string ExpectedResultHash => "0a4d55a8d778e5022fab701977c5d840bbc486d0";
    }
}