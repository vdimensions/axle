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
        public class ConfiguredModuleSection
        {
            public bool Configured { get; set; }
        }
        public sealed class ConfiguredModuleSection2 : ConfiguredModuleSection
        {
            public bool Inherited { get; set; }
        }


        [ModuleConfigSection(typeof(ConfiguredModuleSection), "configuredModule")]
        private sealed class ConfiguredModule
        {
            private readonly ConfiguredModuleSection _config;

            public ConfiguredModule(ConfiguredModuleSection config = null)
            {
                _config = config;
            }

            [ModuleInit]
            internal void Init(ModuleExporter exporter)
            {
                if (_config != null)
                {
                    exporter.Export(_config);
                }
            }
        }
        [ModuleConfigSection(typeof(ConfiguredModuleSection2), "configuredModule")]
        private sealed class ConfiguredModule2
        {
            private readonly ConfiguredModuleSection _config;

            public ConfiguredModule2(ConfiguredModuleSection config = null)
            {
                _config = config;
            }

            [ModuleInit]
            internal void Init(ModuleExporter exporter)
            {
                if (_config != null)
                {
                    exporter.Export(_config);
                }
            }
        }

        [Test]
        public void TestConfigurationSectionDiscovery()
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
                var config = container.Resolve<ConfiguredModuleSection>();

                Assert.IsNotNull(config);
                Assert.IsTrue(config.Configured);
            }
        }

        [Test]
        public void TestInheritedConfigurationSectionDiscovery()
        {
            var cfgPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\appconfig.json";
            var configSource = new StreamedFileConfigSource<JsonConfigurationSource>(File.OpenRead(cfgPath));
            IContainer container = null;
            using (Application
                    .Build()
                    .ConfigureDependencies(c => container = c)
                    .ConfigureApplication(c => c.Append(configSource))
                    .ConfigureModules(c => c.Load<ConfiguredModule2>())
                    .Run())
            {
                var config2 = container.Resolve<ConfiguredModuleSection2>();

                Assert.IsNotNull(config2);
                Assert.IsTrue(config2.Configured);
                Assert.IsTrue(config2.Inherited);
            }
        }

        [Test]
        public void TestConfigurationSectionNotDefinedScenario()
        {
            IContainer container = null;
            using (Application
                    .Build()
                    .ConfigureDependencies(c => container = c)
                    .ConfigureModules(c => c.Load<ConfiguredModule>())
                    .Run())
            {
                var configResolved = container.TryResolve<ConfiguredModuleSection>(out var config);
                Assert.IsFalse(configResolved);
                Assert.IsNull(config);
            }
        }
    }
}
