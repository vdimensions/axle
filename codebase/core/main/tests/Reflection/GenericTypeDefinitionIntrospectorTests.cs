using Axle.Reflection;
using NUnit.Framework;
using System.Collections.Generic;

namespace Axle.Core.Tests.Reflection
{
    [TestFixture]
    public class GenericTypeDefinitionIntrospectorTests
    {
        [Test]
        public void TestGenericTypeDefinitionFlagsAreCorrectForGenericTypeDefinitionTypes()
        {
            var type = typeof(KeyValuePair<,>);
            var introspector = new TypeIntrospector(type);
            var genericTypeDefinitionIntrospector = introspector.GetGenericTypeDefinition();
            Assert.IsTrue(introspector.TypeFlags.IsGenericDefinition());
            Assert.IsNotNull(genericTypeDefinitionIntrospector);
        }

        [Test]
        public void TestGenericTypeDefinitionFlagsAreCorrectForOtherThanGenericTypeDefinitionTypes()
        {
            var type = typeof(string);
            var introspector = new TypeIntrospector(type);
            var genericTypeDefinitionIntrospector = introspector.GetGenericTypeDefinition();
            Assert.IsFalse(introspector.TypeFlags.IsGenericDefinition());
            Assert.IsNull(genericTypeDefinitionIntrospector);
        }

        [Test]
        public void TestGenericTypeDefinitionsDoNotReturnTypeInstrospector()
        {
            var type = typeof(KeyValuePair<,>);
            var introspector = new TypeIntrospector(type);
            var genericTypeDefinitionIntrospector = introspector.GetGenericTypeDefinition();
            var genericTypeDefinitionTypeIntrospector = genericTypeDefinitionIntrospector.Introspect();
            Assert.IsNull(genericTypeDefinitionTypeIntrospector);
        }

        [Test]
        public void TestPartialGenericTypesDoNotReturnTypeInstrospector()
        {
            var type = typeof(KeyValuePair<,>);
            var introspector = new TypeIntrospector(type);
            var genericTypeDefinitionIntrospector = introspector.GetGenericTypeDefinition();
            var partiallyGenericType = genericTypeDefinitionIntrospector.MakeGenericType(typeof(int));
            var genericTypeDefinitionTypeIntrospector = partiallyGenericType.Introspect();
            var fullyGenericType = partiallyGenericType.MakeGenericType(typeof(string)).Introspect();
            Assert.IsNull(genericTypeDefinitionTypeIntrospector);
            Assert.IsNotNull(fullyGenericType);
        }

        [Test]
        public void TestCompleteGenericTypesDoReturnTypeInstrospector()
        {
            var type = typeof(KeyValuePair<,>);
            var introspector = new TypeIntrospector(type);
            var genericTypeDefinitionIntrospector = introspector.GetGenericTypeDefinition();
            var genericTypeDefinitionTypeIntrospector = genericTypeDefinitionIntrospector
                .MakeGenericType(typeof(int), typeof(decimal))
                .Introspect();
            Assert.IsNotNull(genericTypeDefinitionTypeIntrospector);
            Assert.IsTrue(genericTypeDefinitionTypeIntrospector.TypeFlags.IsGeneric());
            Assert.IsFalse(genericTypeDefinitionTypeIntrospector.TypeFlags.IsGenericDefinition());
        }
    }
}
