using Axle;
using Axle.Configuration;
using Axle.DependencyInjection;
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
            using (Application.Build().Load<AB>().Load<BC>().Load<AC>().Run()) { }
        }

        [Test]
        public void TestMultipleModuleInitializations()
        {
            using (Application.Build().Load<AB>().Load<BC>().Load<A>().Load<C>().Load<AC>().Run()) { }
        }

        [Test]
        public void TestModuleConfig()
        {
            IContainer container = null;
            using (Application.Build().ConfigureDependencies(c => container = c).AddLegacyConfig().Run())
            {
                var configuration = container.Resolve<IConfiguration>();
                var message = configuration["message"].Value;
                var messageFormat = configuration["messageFormat"].Value;
                var user = System.Environment.UserName;
                Assert.IsNotNull(configuration);
                Assert.IsNotNull(message);
                Assert.IsNotNull(messageFormat);
                Assert.AreEqual(string.Format(messageFormat, user), message);
            }
        }
    }
}
