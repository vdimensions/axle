#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.Reflection
{
    public interface IRemoveAccessor : IAccessor
    {
        void RemoveDelegate(object target, Delegate handler);
    }
}
#endif