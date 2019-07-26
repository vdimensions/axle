using System;
using System.Reflection;
using Axle.Verification;

namespace Axle.Data.DataSources
{
    public static class SqlScriptLocationRegistryExtensions
    {
        public static ISqlScriptLocationRegistry Register(this ISqlScriptLocationRegistry registry, Uri location)
        {
            registry.VerifyArgument(nameof(registry)).IsNotNull();
            return registry.Register(DataSourceModule.SqlScriptsBundle, location);
        }

        public static ISqlScriptLocationRegistry Register(this ISqlScriptLocationRegistry registry, Assembly assembly, string path)
        {
            registry.VerifyArgument(nameof(registry)).IsNotNull();
            return registry.Register(DataSourceModule.SqlScriptsBundle, assembly, path);
        }
    }
}