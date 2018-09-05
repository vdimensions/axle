#if NETSTANDARD || NET20_OR_NEWER
using System;
#if NETSTANDARD || NET35_OR_NEWER
using System.Linq.Expressions;
#endif


namespace Axle.Reflection
{
    /// <summary>
    /// An interface for an introspector; that is, an utility to provide reflection information on members of a given type.
    /// </summary>
    public interface IIntrospector : IAttributeTarget
    {
        IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes);

        IConstructor[] GetConstructors(ScanOptions scanOptions);

        IMethod GetMethod(ScanOptions scanOptions, string methodName);

        IMethod[] GetMethods(ScanOptions scanOptions);

        IProperty GetProperty(ScanOptions scanOptions, string propertyName);
        #if NETSTANDARD || NET35_OR_NEWER
        IProperty GetProperty(Expression<Func<object>> expression);
        #endif

        IProperty[] GetProperties(ScanOptions scanOptions);

        IField GetField(ScanOptions scanOptions, string fieldName);
        #if NETSTANDARD || NET35_OR_NEWER
        IField GetField(Expression<Func<object>> expression);
        #endif

        IField[] GetFields(ScanOptions scanOptions);

        IEvent GetEvent(ScanOptions scanOptions, string eventName);
        #if NETSTANDARD || NET35_OR_NEWER
        IEvent GetEvent(Expression<Func<object>> expression);
        #endif

        IEvent[] GetEvents(ScanOptions scanOptions);

        IMember[] GetMembers(ScanOptions scanOptions);

        Type IntrospectedType { get; }
    }

    public interface IIntrospector<T> : IIntrospector
    {
        #if NETSTANDARD || NET35_OR_NEWER
        IProperty GetProperty(Expression<Func<T, object>> expression);

        IField GetField(Expression<Func<T, object>> expression);

        IEvent GetEvent(Expression<Func<T, object>> expression);
        #endif
    }
}
#endif