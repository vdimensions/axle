using System;


namespace Axle.Modularity
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public class RequiresAttribute : Attribute
    {
        public RequiresAttribute(Type moduleType)
        {
            ModuleType = moduleType;
        }

        public Type ModuleType { get; }
    }
}