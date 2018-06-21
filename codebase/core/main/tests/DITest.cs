using Axle.Core.DependencyInjection;

using NUnit.Framework;


namespace Axle.Core.Tests
{
    [TestFixture]
    public class DITests
    {
        private class Person
        {
            public Person(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }

        private class Vip : Person
        {
            public Vip(string name) : base(name)
            {
            }
        }

        [Test]
        public void TestContainerInstantiation()
        {
            var c = new Container(null);
            Assert.IsNotNull(c);
            Assert.IsNull(c.Parent);
        }

        [Test]
        public void TestDependencyResolution()
        {
            const string name = "World";

            var c = new Container(null);
            c.RegisterInstance(name);
            c.RegisterType<Person>();

            var p = c.Resolve<Person>();
            Assert.AreEqual(name, p.Name);

            var p2 = c.Resolve<Person>();

            Assert.AreSame(p, p2);
        }

        [Test]
        public void TestParentClassDependencyResolution()
        {
            const string name = "World";

            var c = new Container(null);
            c.RegisterInstance(name);
            c.RegisterType<Vip>();

            var p = c.Resolve<Person>();
            Assert.AreEqual(name, p.Name);

            var p2 = c.Resolve<Person>();

            Assert.AreSame(p, p2);
        }

        [Test]
        public void TestAmbiguousDependencyError()
        {
            Assert.Throws<AmbiguousDependencyException>(
                () =>
                {
                    const string name = "World";

                    var c = new Container(null);
                    c.RegisterInstance(name);
                    c.RegisterType<Person>();
                    c.RegisterType<Vip>();

                    c.Resolve<Person>();
                });
        }

        [Test]
        public void TestDuplicateDependencyError()
        {
            Assert.Throws<DuplicateDependencyDefinitionException>(
                () =>
                {
                    const string name = "World";
                    var c = new Container(null);
                    c.RegisterInstance(name);
                    c.RegisterInstance(name);
                });
        }
    }
}
