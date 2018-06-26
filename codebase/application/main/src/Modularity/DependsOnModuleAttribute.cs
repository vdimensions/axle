using System;


namespace Axle.Application.Modularity
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnModuleAttribute : Attribute
    {
        public DependsOnModuleAttribute(Type moduleType)
        {
            ModuleType = moduleType;
        }

        public Type ModuleType { get; }
    }
}