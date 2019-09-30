using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Axle.Verification;


namespace Axle
{
    partial class Application : IApplicationModuleConfigurer
    {
        IApplicationModuleConfigurer IApplicationModuleConfigurer.Load(IEnumerable<Type> types) => Load(types);
        IApplicationModuleConfigurer IApplicationModuleConfigurer.Load(Assembly assembly)
        {
            assembly.VerifyArgument(nameof(assembly)).IsNotNull();
            return Load(_moduleCatalog.DiscoverModuleTypes(assembly));
        }
        IApplicationModuleConfigurer IApplicationModuleConfigurer.Load(IEnumerable<Assembly> assemblies)
        {
            return Load(assemblies.SelectMany(a => _moduleCatalog.DiscoverModuleTypes(a)));
        }
    }
}
