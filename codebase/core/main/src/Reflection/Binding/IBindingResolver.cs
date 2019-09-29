using System;

namespace Axle.Reflection.Binding
{
    public interface IBindingResolver
    {
        bool TryResolve(Type type, string memberName, out object resolvedValue);
        IBindingResolver CreateNestedResolver(string memberName);
        IBindingResolver Parent { get; }
    }
}