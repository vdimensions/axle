using Axle.Reflection;
using System;

namespace Axle.Conversion.Binding
{
    public interface IBindingMemberInfoProvider
    {
        IReadWriteMember GetMember(object instance, string member);
        IReadWriteMember[] GetMembers(object instance);
        bool TryCreateInstance(Type type, out object instance);
    }
}
