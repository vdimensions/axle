using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Axle.Reflection
{
    public interface IIntrospector
    {
        IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes);

        IEnumerable<IConstructor> GetConstructors(ScanOptions scanOptions);

        IMethod GetMethod(ScanOptions scanOptions, string methodName);

        IEnumerable<IMethod> GetMethods(ScanOptions scanOptions);

        IProperty GetProperty(ScanOptions scanOptions, string propertyName);
        IProperty GetProperty(Expression<Func<object>> expression);

        IEnumerable<IProperty> GetProperties(ScanOptions scanOptions);

        IField GetField(ScanOptions scanOptions, string fieldName);
        IField GetField(Expression<Func<object>> expression);

        IEnumerable<IField> GetFields(ScanOptions scanOptions);

        IEvent GetEvent(ScanOptions scanOptions, string eventName);
        IEvent GetEvent(Expression<Func<object>> expression);

        IEnumerable<IEvent> GetEvents(ScanOptions scanOptions);

        IEnumerable<IMember> GetMembers(ScanOptions scanOptions);

        Type IntrospectedType { get; }
    }

    public interface IIntrospector<T> : IIntrospector
    {
        IProperty GetProperty(Expression<Func<T, object>> expression);

        IField GetField(Expression<Func<T, object>> expression);

        IEvent GetEvent(Expression<Func<T, object>> expression);
    }
}