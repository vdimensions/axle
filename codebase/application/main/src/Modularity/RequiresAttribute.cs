using System;


namespace Axle.Modularity
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public class RequiresAttribute : Attribute, IModuleReferenceAttribute
    {
        public RequiresAttribute(Type moduleType)
        {
            ModuleType = moduleType;
        }

        bool IModuleReferenceAttribute.Mandatory => true;
        public Type ModuleType { get; }
    }
}