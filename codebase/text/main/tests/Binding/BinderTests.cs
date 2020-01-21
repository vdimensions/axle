using System;
using System.Xml.Linq;
using Axle.Text.StructuredData.Binding;
using Axle.Text.StructuredData.Xml;
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
            public Pet[] Pets { get; set; }
        }

        public const string XmlFormat = @"
        <Owner>
            <Name>{0}</Name>
            <Pets>
                <Name>{1}</Name>
                <Type>{2}</Type>
            </Pets>
        </Owner>
        ";

        [Test]
        public void TestDefaultBinder()
        {
            const string ownerName = "Diana";
            const string petName = "Lion";
            const string petType = "cat";

            var xmlBindingSource = String.Format(XmlFormat, ownerName, petName, petType);
            var provider = new TextDataProvider(new XDocumentDataReader(StringComparer.OrdinalIgnoreCase).Read(xmlBindingSource));
            var binder = new DefaultBinder();
            Owner owner = new Owner();
            owner = (Owner) binder.Bind(provider, owner);

            Assert.AreEqual(ownerName, owner.Name, "Owner name does not match");
            Assert.AreEqual(1, owner.Pets.Length, "Pet's count does not match");
            Assert.AreEqual(petName, owner.Pets[0].Name, "Pet name does not match");
            Assert.AreEqual(petType, owner.Pets[0].Type, "Pet type does not match");
        }
    }
}
