using Axle.Logging;
using Axle.Modularity;
using NUnit.Framework;


namespace Axle.ApplicationTests
{
    [TestFixture]
    public class ModularityTests
    {
        public abstract class AbstractModule
        {
            [ModuleInit]
            public void Init()
            {
                //Logger.Debug($"{GetType().Name} is initialized");
            }

            [ModuleDependencyInitialized]
            public void OnDependencyInitialized(object x)
            {
                //Logger.Debug($"{x.GetType().Name} notifies {GetType().Name} for being initialized");
            }

            [ModuleDependencyTerminated]
            public void OnDependencyTerminated(object x)
            {
                //Logger.Debug($"{x.GetType().Name} notifies {GetType().Name} for getting terminated");
            }

            public ILogger Logger { get; set; }
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
            using (Application.Build().Load(typeof(AB)).Load(typeof(BC)).Load(typeof(AC)).Run()) { }
        }

        [Test]
        public void TestMultipleModuleInitializations()
        {
            using (Application.Build().Load(typeof(AB)).Load(typeof(BC)).Load(typeof(A)).Load(typeof(C)).Load(typeof(AC)).Run()) { }
        }
    }
}
