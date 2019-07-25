using System;
using System.Collections.Generic;
using System.Reflection;

namespace Axle
{
    public interface IApplicationModuleConfigurer : IApplicationBuilder
    {
        IApplicationModuleConfigurer Load(IEnumerable<Type> types);
        IApplicationModuleConfigurer Load(Assembly assembly);
        IApplicationModuleConfigurer Load(IEnumerable<Assembly> assemblies);
    }
}