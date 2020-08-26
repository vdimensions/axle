namespace Axle.Security.Cryptography.Tests.Algorithms.Hash
{
    public abstract class AbstractMD5IntegrityTest : AbstractHashIntegrityTest
    {
        protected override string Value => "Hello World";

        protected override string ExpectedResultHash => "b10a8db164e0754105b7a99be72e3fe5";
    }
}