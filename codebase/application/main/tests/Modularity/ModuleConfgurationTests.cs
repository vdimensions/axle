using NUnit.Framework;
using Axle.Modularity;
using System.Reflection;
using System.IO;
using Axle.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Axle.DependencyInjection;

namespace Axle.ApplicationTests.Modularity
{
    [TestFixture]
    public class ModuleConfgurationTests
    {
        public sealed class ConfiguredModuleSection
        {
            public bool Configured { get; set; }
        }


        [ModuleConfigSection(typeof(ConfiguredModuleSection), "configuredModule")]
        private sealed class ConfiguredModule
        {
            private readonly ConfiguredModuleSection _config;

            public ConfiguredModule(ConfiguredModuleSection config)
            {
                _config = config;
            }

            [ModuleInit]
            internal void Init(ModuleExporter exporter)
            {
                exporter.Export(_config);
            }
        }

        [Test]
        public void TestConfigurationSectionIsDiscovered()
        {
            var cfgPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\appconfig.json";
            var configSource = new StreamedFileConfigSource<JsonConfigurationSource>(File.OpenRead(cfgPath));
            IContainer container = null;
            using (Application
                    .Build()
                    .ConfigureDependencies(c => container = c)
                    .ConfigureApplication(c => c.Append(configSource))
                    .ConfigureModules(c => c.Load<ConfiguredModule>())
                    .Run())
            {
                Assert.IsNotNull(container);

                var config = container.Resolve<ConfiguredModuleSection>();
                Assert.IsNotNull(config);
                Assert.IsTrue(config.Configured);
            }
        }
    }
}
