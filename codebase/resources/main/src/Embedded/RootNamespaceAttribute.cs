using System;

namespace Axle.Resources.Embedded
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class RootNamespaceAttribute : Attribute
    {
        public RootNamespaceAttribute(string @namespace)
        {
            Namespace = @namespace;
        }

        public string Namespace { get; }
    }
}