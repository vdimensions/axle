using NUnit.Framework;

using System;

using Axle.Application.Modularity;


namespace Axle.Application.Tests
{
    [TestFixture]
    public class TestClass
    {
        public abstract class AbstractModule
        {
            [ModuleInit]
            public void Init()
            {
                Console.WriteLine($"{GetType().Name} is initialized");
            }

            [ModuleDependencyInitialized]
            public void OnDependencyInitialized(object x)
            {
                Console.WriteLine($"{x.GetType().Name} notifies {GetType().Name} for being initialized");
            }

            [ModuleDependencyTerminated]
            public void OnDependencyTerminated(object x)
            {
                Console.WriteLine($"{x.GetType().Name} notifies {GetType().Name} for getting terminated");
            }

            [ModuleReady]
            public void Ready()
            {
                Console.WriteLine($"{GetType().Name} is ready");
            }
        }

        [Module]
        public class A : AbstractModule { }

        [Module]
        public class B : AbstractModule { }

        [Module]
        public class C : AbstractModule { }

        [Requires(typeof(A))]
        [Requires(typeof(B))]
        [Module]
        public class AB : AbstractModule { }

        [Requires(typeof(A))]
        [Requires(typeof(C))]
        [Module]
        public class AC : AbstractModule { }

        [Requires(typeof(B))]
        [Requires(typeof(C))]
        [Module]
        public class BC : AbstractModule { }

        [Test]
        public void TestModuleInitialization()
        {
            var c = new ModularContext();
            c.Launch(typeof(AB), typeof(AC), typeof(BC));
        }

        [Test]
        public void TestMultipleModuleInitializations()
        {
            var c = new ModularContext();
            c.Launch(typeof(AB));
            c.Launch(typeof(AC));
            c.Launch(typeof(BC));
        }
    }
}
