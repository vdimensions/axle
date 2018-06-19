using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Axle.Core.Infrastructure.DependencyInjection;

using NUnit.Framework;


namespace Axle.Core.Tests
{
    [TestFixture]
    public class DITest
    {
        private class Person
        {
            public Person(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }

        [Test]
        public void TestContainerInstantiation()
        {
            var c = new Container(null);
            Assert.IsNotNull(c);
            Assert.IsNull(c.Parent);
        }


        [Test]
        public void TestContainerDI()
        {
            var c = new Container(null);
            c.RegisterInstance("World");
            c.RegisterType<Person>();

            var p = c.Resolve<Person>();
            Assert.AreEqual("World", p.Name);
        }
    }
}
