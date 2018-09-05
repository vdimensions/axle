#if NETSTANDARD || NET20_OR_NEWER
using System;


namespace Axle.Reflection
{
    public interface IGenericMethod : IMethod
    {
        Type[] GenericArguments { get; }
    }
}
#endif