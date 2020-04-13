using System.Collections.Generic;
using System.Linq;
using Axle.Configuration;
using Axle.Data.Configuration;
using Axle.Data.DataSources;
using Axle.DependencyInjection;
using Axle.Modularity;
using NUnit.Framework;

namespace Axle.Data.MySql.Tests
{
    public class ModularityTests
    {
        [Module]
        private class SqlScriptsConfigurer : ISqlScriptLocationConfigurer
        {
            public void RegisterScriptLocations(ISqlScriptLocationRegistry registry)
            {
                registry.Register("test", GetType().Assembly, "SqlScripts/");
            }
        }
        
        [Test]
        public void TestMySqlProviderIsRegistered()
        {
            IDependencyContainer dependencyContainer = null;
            var appBuilder = Application.Build()
                .ConfigureDependencies(c => dependencyContainer = c)
                .UseMySql();
            using (appBuilder.Run())
            {
                var providers = dependencyContainer.Resolve<IEnumerable<IDbServiceProvider>>().ToArray();
                Assert.IsNotEmpty(providers, "No database service providers have been registered");
                Assert.True(providers.Length == 1, "Only one database service provider is expected.");
                Assert.AreEqual(providers[0].ProviderName, MySqlServiceProvider.Name);
            }
        }
        
        [Test]
        public void TestConnectionStringBinding()
        {
            IDependencyContainer dependencyContainer = null;
            var appBuilder = Application.Build()
                .ConfigureDependencies(c => dependencyContainer = c)
                .EnableLegacyConfig()
                .UseMySql();
            using (appBuilder.Run())
            {
                var cfg = dependencyContainer.Resolve<IConfiguration>();
                var connectionStrings = cfg.GetConnectionStrings();
                var defaultConnectionString = connectionStrings.Where(x => x.Name.Equals("default"));

                Assert.IsNotNull(defaultConnectionString);
            }
        }
        
        [Test]
        public void TestConnectionStringsBinding()
        {
            IDependencyContainer dependencyContainer = null;
            var appBuilder = Application.Build()
                .ConfigureDependencies(c => dependencyContainer = c)
                .EnableLegacyConfig()
                .UseMySql();
            using (appBuilder.Run())
            {
                var cfg = dependencyContainer.Resolve<IConfiguration>();
                var connectionStrings = cfg.GetConnectionStrings();

                Assert.IsNotNull(connectionStrings);
                Assert.IsNotEmpty(connectionStrings);
            }
        }

        [Test]
        public void TestDataSourceDiscovery()
        {
            IDependencyContainer dependencyContainer = null;
            var appBuilder = Application.Build()
                .ConfigureDependencies(c => dependencyContainer = c)
                .EnableLegacyConfig()
                .UseDataSources()
                .UseMySql();
            using (appBuilder.Run())
            {
                var cfg = dependencyContainer.Resolve<IConfiguration>();
                var connectionStrings = cfg.GetConnectionStrings();

                Assert.IsNotNull(connectionStrings);

                foreach (var connectionStringInfo in connectionStrings)
                {
                    var resolved = dependencyContainer.TryResolve<DataSource>(out var dataSource, connectionStringInfo.Name);
                    Assert.IsTrue(resolved);
                    Assert.IsNotNull(dataSource);
                    Assert.AreEqual(dataSource.Name, connectionStringInfo.Name);
                }
            }
        }

        [Test]
        public void TestDataSourceCmdDiscovery()
        {
            IDependencyContainer dependencyContainer = null;
            var appBuilder = Application.Build()
                .ConfigureDependencies(c => dependencyContainer = c)
                .EnableLegacyConfig()
                .UseDataSources()
                .UseMySql()
                .ConfigureModules(m => m.Load<SqlScriptsConfigurer>());
            using (appBuilder.Run())
            {
                var testDataSource = dependencyContainer.Resolve<IDataSource>("default");
                var script = testDataSource.GetScript("test", "MySqlTestScript");
                Assert.IsNotNull(script);
            }
        }
    }
}