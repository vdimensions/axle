using System;
using System.Reflection;


namespace Axle.Reflection
{
    /// <summary>
    /// An interface for a type introspector; that is, an utility to provide reflection information on members of a 
    /// given type.
    /// </summary>
    public interface ITypeIntrospector : IAttributeTarget
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
        /// Creates a <see cref="IConstructor"/> instance from the provided <paramref name="reflectedConstructor"/>.
        /// </summary>
        /// <param name="reflectedConstructor">
        /// An already reflected <see cref="ConstructorInfo"/>.
        /// </param>
        /// <returns>
        /// A <see cref="IConstructor"/> instance created from the supplied <paramref name="reflectedConstructor"/>.
        /// </returns>
        IConstructor GetConstructor(ConstructorInfo reflectedConstructor);

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
        /// <summary>
        /// Creates a <see cref="IMethod"/> instance from the provided <paramref name="reflectedMethod"/>.
        /// </summary>
        /// <param name="reflectedMethod">
        /// An already reflected <see cref="MethodInfo"/>.
        /// </param>
        /// <returns>
        /// A <see cref="IMethod"/> instance created from the supplied <paramref name="reflectedMethod"/>.
        /// </returns>
        IMethod GetMethod(MethodInfo reflectedMethod);

        IMethod[] GetMethods(ScanOptions scanOptions);

        IProperty GetProperty(ScanOptions scanOptions, string propertyName);
        /// <summary>
        /// Creates a <see cref="IProperty"/> instance from the provided <paramref name="reflectedProperty"/>.
        /// </summary>
        /// <param name="reflectedProperty">
        /// An already reflected <see cref="PropertyInfo"/>.
        /// </param>
        /// <returns>
        /// A <see cref="IProperty"/> instance created from the supplied <paramref name="reflectedProperty"/>.
        /// </returns>
        IProperty GetProperty(PropertyInfo reflectedProperty);

        IProperty[] GetProperties(ScanOptions scanOptions);

        IField GetField(ScanOptions scanOptions, string fieldName);
        /// <summary>
        /// Creates a <see cref="IField"/> instance from the provided <paramref name="reflectedField"/>.
        /// </summary>
        /// <param name="reflectedField">
        /// An already reflected <see cref="FieldInfo"/>.
        /// </param>
        /// <returns>
        /// A <see cref="IField"/> instance created from the supplied <paramref name="reflectedField"/>.
        /// </returns>
        IField GetField(FieldInfo reflectedField);

        IField[] GetFields(ScanOptions scanOptions);

        IEvent GetEvent(ScanOptions scanOptions, string eventName);
        /// <summary>
        /// Creates a <see cref="IEvent"/> instance from the provided <paramref name="reflectedEvent"/>.
        /// </summary>
        /// <param name="reflectedEvent">
        /// An already reflected <see cref="EventInfo"/>.
        /// </param>
        /// <returns>
        /// A <see cref="IEvent"/> instance created from the supplied <paramref name="reflectedEvent"/>.
        /// </returns>
        IEvent GetEvent(EventInfo reflectedEvent);

        IEvent[] GetEvents(ScanOptions scanOptions);

        IMember[] GetMembers(ScanOptions scanOptions);

        /// <summary>
        /// Gets a <see cref="IGenericTypeIntrospector"/> for accessing the type's generic definition,
        /// if the <see cref="IntrospectedType"/> is a generic type, or <c><see langword="null"/></c> otherwise.
        /// </summary>
        /// <returns>
        /// A <see cref="IGenericTypeIntrospector"/> if the <see cref="IntrospectedType"/> is a generic type;
        /// otherwise <c><see langword="null"/></c>
        /// </returns>
        /// <seealso cref="IsGenericType"/>
        /// <seealso cref="IsGenericTypeDefinition"/>
        /// <seealso cref="IGenericTypeIntrospector"/>
        IGenericTypeIntrospector GetGenericTypeDefinition();

        /// <summary>
        /// The <see cref="Type"/> which the current <see cref="IIntrospector"/> instance provides reflected information for.
        /// </summary>
        Type IntrospectedType { get; }

        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Gets the underlying <see cref="TypeCode"/> for the <see cref="IntrospectedType"/>.
        /// </summary>
        TypeCode TypeCode { get; }
        #endif
        
        /// <summary>
        /// Checks whether the specified <see cref="IntrospectedType"/> is a generic type.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> is a generic type; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsGenericType { get; }

        /// <summary>
        /// Checks whether the specified <see cref="IntrospectedType"/> is a generic type definition.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> is a generic type definition; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsGenericTypeDefinition { get; }

        /// <summary>
        /// Determines if the <see cref="IntrospectedType"/> is a delegate.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the provided <see cref="IntrospectedType"/> is a delegate;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsDelegate { get; }

        /// <summary>
        /// Checks whether the specified <see cref="IntrospectedType"/> is a nullable type.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> is a nullable type; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsNullableType { get; }

        /// <summary>
        /// Checks whether the specified <see cref="IntrospectedType"/> is abstract.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> is abstract; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsAbstract { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IntrospectedType"/> represents an enumeration.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> represents an enumeration; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        bool IsEnum { get; }
    }

    /// <summary>
    /// A generic version of the <see cref="ITypeIntrospector"/> interface.
    /// </summary>
    /// <typeparam name="T">
    /// The introspected by the current <see cref="ITypeIntrospector{T}"/> instance type.
    /// </typeparam>
    public interface ITypeIntrospector<T> : ITypeIntrospector { }
}
