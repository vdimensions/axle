using System;
using System.Reflection;

namespace Axle.Data.Resources
{
    public interface ISqlScriptLocationConfigurer
    {
        void RegisterScriptLocations(ISqlScriptLocationRegistry bundle);
    }

    public interface ISqlScriptLocationRegistry
    {
        ISqlScriptLocationRegistry Register(Uri location);
        ISqlScriptLocationRegistry Register(Assembly assembly, string path);
    }
}
