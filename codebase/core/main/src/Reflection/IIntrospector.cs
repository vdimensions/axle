using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Axle.Reflection
{
    /// <summary>
    /// An interface for an introspector; that is, an utility to provide reflection information on members of a given type.
    /// </summary>
    public interface IIntrospector
    {
        IAttributeInfo[] GetAttributes();
        IAttributeInfo[] GetAttributes(Type attributeType);

        IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes);

        IConstructor[] GetConstructors(ScanOptions scanOptions);

        IMethod GetMethod(ScanOptions scanOptions, string methodName);

        IMethod[] GetMethods(ScanOptions scanOptions);

        IProperty GetProperty(ScanOptions scanOptions, string propertyName);
        IProperty GetProperty(Expression<Func<object>> expression);

        IProperty[] GetProperties(ScanOptions scanOptions);

        IField GetField(ScanOptions scanOptions, string fieldName);
        IField GetField(Expression<Func<object>> expression);

        IField[] GetFields(ScanOptions scanOptions);

        IEvent GetEvent(ScanOptions scanOptions, string eventName);
        IEvent GetEvent(Expression<Func<object>> expression);

        IEvent[] GetEvents(ScanOptions scanOptions);

        IMember[] GetMembers(ScanOptions scanOptions);

        Type IntrospectedType { get; }
    }

    public interface IIntrospector<T> : IIntrospector
    {
        IProperty GetProperty(Expression<Func<T, object>> expression);

        IField GetField(Expression<Func<T, object>> expression);

        IEvent GetEvent(Expression<Func<T, object>> expression);
    }
}