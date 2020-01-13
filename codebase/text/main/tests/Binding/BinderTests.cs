using System;
using System.Xml.Linq;
using Axle.Text.StructuredData.Binding;
using NUnit.Framework;

namespace Axle.Text.StructuredData.Tests.Binding
{
    [TestFixture]
    public class BinderTests
    {
        public sealed class Pet
        {
            public string Type { get; set; }
            public string Name { get; set; }
        }
        public class Owner
        {
            public string Name { get; set; }
            public Pet Pet { get; set; }
        }

        public class SimpleValueProvider : ISimpleMemberValueProvider
        {
            public SimpleValueProvider(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }
            public string Value { get; }
        }

        public class XmlValueProvider : IComplexMemberValueProvider
        {
            private readonly XElement _xElement;

            public XmlValueProvider(XElement xElement, string name)
            {
                _xElement = xElement;
                Name = name;
            }

            public bool TryGetValue(string member, out IBindingValueProvider value)
            {
                var childElement = _xElement.Element(XName.Get(member));
                if (childElement != null)
                {
                    if (childElement.HasElements)
                    {
                        value = new XmlValueProvider(childElement, member);
                    }
                    else
                    {
                        value = new SimpleValueProvider(member, childElement.Value);
                    }
                    return true;
                }
                value = null;
                return false;
            }

            public string Name { get; }
            public IBindingValueProvider this[string member] => TryGetValue(member, out var result) ? result : null;
        }

        public const string XmlFormat = @"
        <Owner>
            <Name>{0}</Name>
            <Pet>
                <Name>{1}</Name>
                <Type>{2}</Type>
            </Pet>
        </Owner>
        ";

        [Test]
        public void TestDefaultBinder()
        {
            const string ownerName = "Diana";
            const string petName = "Lion";
            const string petType = "cat";

            var xmlBindingSource = String.Format(XmlFormat, ownerName, petName, petType);
            var provider = new XmlValueProvider(XDocument.Parse(xmlBindingSource).Root, String.Empty);
            var binder = new DefaultBinder();
            Owner owner = new Owner();
            owner = (Owner) binder.Bind(provider, owner);

            Assert.AreEqual(ownerName, owner.Name, "Owner name does not match");
            Assert.AreEqual(petName, owner.Pet.Name, "Pet name does not match");
            Assert.AreEqual(petType, owner.Pet.Type, "Pet type does not match");
        }
    }
}
