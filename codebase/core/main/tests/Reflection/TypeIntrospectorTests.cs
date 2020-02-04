using Axle.Reflection;
using NUnit.Framework;

namespace Axle.Core.Tests.Reflection
{
    [TestFixture]
    public class TypeIntrospectorTests
    {
        public class PublicClass { }
        internal class InternalClass { }
        protected class ProtectedClass { }
        protected internal class ProtectedInternalClass { }
        private class PrivateClass { }

        [Test]
        public void TestTypeIntrospectorCorrectlyResolvesPublicTypeAccessModifier()
        {
            var introspector = new TypeIntrospector(typeof(PublicClass));
            Assert.AreEqual(AccessModifier.Public, introspector.AccessModifier, "Public AccessModifier is not correctly resolved");
        }

        [Test]
        public void TestTypeIntrospectorCorrectlyResolvesInternalTypeAccessModifier()
        {
            var introspector = new TypeIntrospector(typeof(InternalClass));
            Assert.AreEqual(AccessModifier.Internal, introspector.AccessModifier, "Internal AccessModifier is not correctly resolved");
        }

        [Test]
        public void TestTypeIntrospectorCorrectlyResolvesProtectedTypeAccessModifier()
        {
            var introspector = new TypeIntrospector(typeof(ProtectedClass));
            Assert.AreEqual(AccessModifier.Protected, introspector.AccessModifier, "Protected AccessModifier is not correctly resolved");
        }

        [Test]
        public void TestTypeIntrospectorCorrectlyResolvesProtectedInternalTypeAccessModifier()
        {
            var introspector = new TypeIntrospector(typeof(ProtectedInternalClass));
            Assert.AreEqual(AccessModifier.ProtectedInternal, introspector.AccessModifier, "ProtectedInternal AccessModifier is not correctly resolved");
        }

        [Test]
        public void TestTypeIntrospectorCorrectlyResolvesPrivateTypeAccessModifier()
        {
            var introspector = new TypeIntrospector(typeof(PrivateClass));
            Assert.AreEqual(AccessModifier.Private, introspector.AccessModifier, "Private AccessModifier is not correctly resolved");
        }
    }
}
