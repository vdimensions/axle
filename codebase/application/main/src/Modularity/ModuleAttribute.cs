using System;


namespace Axle.Application.Modularity
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ModuleAttribute : Attribute
    {
        public ModuleAttribute(string name)
        {
            Name = name;
        }
        public ModuleAttribute() : this(string.Empty) { }

        public string Name { get; }
    }
}