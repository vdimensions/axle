using System;


namespace Axle.Modularity
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public class RequiresAttribute : Attribute, IUsesAttribute
    {
        public RequiresAttribute(Type moduleType)
        {
            ModuleType = moduleType;
        }

        bool IUsesAttribute.Required => true;
        public Type ModuleType { get; }
    }
}