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
        /// <summary>
        /// Looks up a <see cref="IConstructor"/> for the <see cref="IntrospectedType">introspected type</see>
        /// that matches the provided <paramref name="scanOptions"/> and a signature conforming to the specified <paramref name="argumentTypes"/>.
        /// </summary>
        /// <param name="scanOptions">
        /// One of the <see cref="ScanOptions"/> values, representing the reflection lookup flags.
        /// </param>
        /// <param name="argumentTypes">
        /// An array of types to match the signature of the constructor being looked up.
        /// </param>
        /// <returns>
        /// An instance of <see cref="IConstructor"/> representing the reflected constructor, or <c>null</c> if the lookup did not yield any results.
        /// </returns>
        IConstructor GetConstructor(ScanOptions scanOptions, params Type[] argumentTypes);

        /// <summary>
        /// Looks up a collection of <see cref="IConstructor">constructors</see> for the <see cref="IntrospectedType">introspected type</see>
        /// that match the provided <paramref name="scanOptions"/>.
        /// </summary>
        /// <param name="scanOptions">
        /// One of the <see cref="ScanOptions"/> values, representing the reflection lookup flags.
        /// </param>
        /// <returns>
        /// An array of <see cref="IConstructor"/> representing the reflected constructors, or an empty array if the lookup did not yield any results.
        /// </returns>
        IConstructor[] GetConstructors(ScanOptions scanOptions);

        IMethod GetMethod(ScanOptions scanOptions, string methodName);

        IMethod[] GetMethods(ScanOptions scanOptions);

        IProperty GetProperty(ScanOptions scanOptions, string propertyName);
        #if NETSTANDARD || NET35_OR_NEWER
        IProperty GetProperty<TResult>(Expression<Func<TResult>> expression);
        #endif

        IProperty[] GetProperties(ScanOptions scanOptions);

        IField GetField(ScanOptions scanOptions, string fieldName);
        #if NETSTANDARD || NET35_OR_NEWER
        IField GetField<TResult>(Expression<Func<TResult>> expression);
        #endif

        IField[] GetFields(ScanOptions scanOptions);

        IEvent GetEvent(ScanOptions scanOptions, string eventName);
        #if NETSTANDARD || NET35_OR_NEWER
        IEvent GetEvent<TResult>(Expression<Func<TResult>> expression);
        #endif

        IEvent[] GetEvents(ScanOptions scanOptions);

        IMember[] GetMembers(ScanOptions scanOptions);

        /// <summary>
        /// The <see cref="Type"/> which the current <see cref="IIntrospector"/> instance provides reflected information for.
        /// </summary>
        Type IntrospectedType { get; }
    }

    public interface IIntrospector<T> : IIntrospector
    {
        #if NETSTANDARD || NET35_OR_NEWER
        IProperty GetProperty<TResult>(Expression<Func<T, TResult>> expression);

        IField GetField<TResult>(Expression<Func<T, TResult>> expression);

        IEvent GetEvent<TResult>(Expression<Func<T, TResult>> expression);
        #endif
    }
}
