using System.Linq;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.Modularity;
using NUnit.Framework;


namespace Axle.ApplicationTests.Modularity
{
    [TestFixture]
    public class ModuleLoadingTests
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
            using (Application.Build().ConfigureModules(c => c.Load<AB>().Load<BC>().Load<AC>()).Run()) { }
        }

        [Test]
        public void TestMultipleModuleInitializations()
        {
            using (Application.Build().ConfigureModules(c => c.Load<AB>().Load<BC>().Load<A>().Load<C>().Load<AC>()).Run()) { }
        }

        [Test]
        public void TestModuleConfig()
        {
            IDependencyContainer dependencyContainer = null;
            using (Application.Build()
                .ConfigureDependencies(c => dependencyContainer = c)
                .ConfigureApplication(c => c.EnableLegacyConfig())
                .Run())
            {
                var configuration = dependencyContainer.Resolve<IConfiguration>();
                var message = configuration["message"].Select(x => x.Value).SingleOrDefault();
                var messageFormat = (string) configuration["messageFormat"].Select(x => x.Value).SingleOrDefault();
                var user = System.Environment.UserName;
                Assert.IsNotNull(configuration);
                Assert.IsNotNull(message);
                Assert.IsNotNull(messageFormat);
                Assert.AreEqual(string.Format(messageFormat, user), message);
            }
        }
    }
}
