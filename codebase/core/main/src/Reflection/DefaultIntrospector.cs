#if NETSTANDARD || NET35_OR_NEWER
#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;


namespace Axle.Reflection
{
    [Obsolete("Use TypeIntrospector instead")]
    public sealed class DefaultIntrospector : TypeIntrospector, IIntrospector
    {
        public DefaultIntrospector(Type introspectedType) : base(introspectedType) { }
    }

    [Obsolete("Use TypeIntrospector<T> instead")]
    public sealed class DefaultIntrospector<T> : TypeIntrospector<T>, IIntrospector<T>
    {
        public DefaultIntrospector() : base() { }
    }
}
#endif
#endif