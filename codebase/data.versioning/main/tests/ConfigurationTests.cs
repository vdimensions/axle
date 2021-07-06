using System;
using Axle.Configuration;
using Axle.Configuration.Text.Documents;
using Axle.Data.Versioning.Configuration;
using Axle.Text.Documents.Binding;
using Axle.Text.Documents.Yaml;
using NUnit.Framework;

namespace Axle.Data.Versioning.Tests
{
    public class ConfigurationTests
    {
        [Test]
        public void TestSingleChangelogConfig()
        {
            var config = @"
axle.data.versioning.migrations:
 - changelog:
    - change1
    - change2
   datasource: mssql
";
            var cfg = new LayeredConfigManager()
                .Append(new TextDocumentConfigSource(new YamlDocumentReader(StringComparer.OrdinalIgnoreCase), config))
                .LoadConfiguration();
            var section = cfg.GetSection<DbVersioningConfig>("axle.data.versioning");
            
            Assert.IsNotNull(section, "unable to parse configuration section");
            Assert.AreEqual(1, section.Migrations.Count, "unable to retrieve migration settings");
        }
        
        [Test]
        public void TestMultipleChangelogConfig()
        {
            var config = @"
axle.data.versioning.migrations:
 - changelog:
    - change1
    - change2
   datasource: mssql
 - changelog:
    - change1
    - change2
   datasource: mariadb
 - changelog:
    - change1
    - change2
   datasource: postgresql
";
            var cfg = new LayeredConfigManager()
                .Append(new TextDocumentConfigSource(new YamlDocumentReader(StringComparer.OrdinalIgnoreCase), config))
                .LoadConfiguration();
            var section = cfg.GetSection<DbVersioningConfig>("axle.data.versioning");
            
            Assert.IsNotNull(section, "unable to parse configuration section");
            Assert.AreEqual(3, section.Migrations.Count, "unable to retrieve migration settings");
        }
    }
}