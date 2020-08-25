using System;
using Axle.Text.Documents.Binding;
using Axle.Text.Documents.Xml;
using NUnit.Framework;

namespace Axle.Text.Documents.Tests.Binding
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
                <Type>{2}</Type>
                <Name>{1}</Name>
            </Pets>
        </Owner>
        ";

        [Test]
        public void TestDefaultBinderWithXmlTextDocumentReader()
        {
            const string ownerName = "Diana";
            const string petName = "Lion";
            const string petType = "cat";

            var xmlBindingSource = string.Format(XmlFormat, ownerName, petName, petType);
            var binder = new DefaultDocumentBinder();
            var owner = (Owner) binder.Bind(
                new XmlTextDocumentReader(StringComparer.OrdinalIgnoreCase).Read(xmlBindingSource), 
                new Owner());

            Assert.AreEqual(ownerName, owner.Name, "Owner name does not match");
            Assert.AreEqual(1, owner.Pets.Length, "Pet's count does not match");
            Assert.AreEqual(petName, owner.Pets[0].Name, "Pet name does not match");
            Assert.AreEqual(petType, owner.Pets[0].Type, "Pet type does not match");
        }
        
        [Test]
        public void TestDefaultBinderWithXDocumentReader()
        {
            const string ownerName = "Diana";
            const string petName = "Lion";
            const string petType = "cat";

            var xmlBindingSource = string.Format(XmlFormat, ownerName, petName, petType);
            var binder = new DefaultDocumentBinder();
            var owner = (Owner) binder.Bind(
                new XDocumentReader(StringComparer.OrdinalIgnoreCase).Read(xmlBindingSource), 
                new Owner());

            Assert.AreEqual(ownerName, owner.Name, "Owner name does not match");
            Assert.AreEqual(1, owner.Pets.Length, "Pet's count does not match");
            Assert.AreEqual(petName, owner.Pets[0].Name, "Pet name does not match");
            Assert.AreEqual(petType, owner.Pets[0].Type, "Pet type does not match");
        }
    }
}
