using System;


namespace Axle.Reflection
{
    [Obsolete("Use ITypeIntrospector instead")]
    public interface IIntrospector : ITypeIntrospector { }

    [Obsolete("Use ITypeIntrospector<T> instead")]
    public interface IIntrospector<T> : IIntrospector, ITypeIntrospector<T> { }
}
