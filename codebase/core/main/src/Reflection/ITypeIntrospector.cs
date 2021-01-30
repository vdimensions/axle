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

        /// <summary>
        /// Gets an array representing the properties that are declared or inherited by the <see cref="IntrospectedType"/>,
        /// filtered in accordance to the provided <paramref name="scanOptions"/>.
        /// </summary>
        /// <param name="scanOptions">
        /// One, or a combination of the <see cref="ScanOptions"/> values representing the reflection search criteria.
        /// </param>
        /// <returns>
        /// An array representing the properties that are declared or inherited by the <see cref="IntrospectedType"/>,
        /// filtered in accordance to the provided <paramref name="scanOptions"/>.
        /// </returns>
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

        /// <summary>
        /// Gets an array representing the fields that are declared or inherited by the <see cref="IntrospectedType"/>,
        /// filtered in accordance to the provided <paramref name="scanOptions"/>.
        /// </summary>
        /// <param name="scanOptions">
        /// One, or a combination of the <see cref="ScanOptions"/> values representing the reflection search criteria.
        /// </param>
        /// <returns>
        /// An array representing the fields that are declared or inherited by the <see cref="IntrospectedType"/>,
        /// filtered in accordance to the provided <paramref name="scanOptions"/>.
        /// </returns>
        IField[] GetFields(ScanOptions scanOptions);

        IEvent GetEvent(ScanOptions scanOptions, string eventName);
        /// <summary>
        /// Creates an <see cref="IEvent"/> instance from the provided <paramref name="reflectedEvent"/>.
        /// </summary>
        /// <param name="reflectedEvent">
        /// An already reflected <see cref="EventInfo"/>.
        /// </param>
        /// <returns>
        /// A <see cref="IEvent"/> instance created from the supplied <paramref name="reflectedEvent"/>.
        /// </returns>
        IEvent GetEvent(EventInfo reflectedEvent);

        /// <summary>
        /// Gets an array representing the events that are declared or inherited by the <see cref="IntrospectedType"/>,
        /// filtered in accordance to the provided <paramref name="scanOptions"/>.
        /// </summary>
        /// <param name="scanOptions">
        /// One, or a combination of the <see cref="ScanOptions"/> values representing the reflection search criteria.
        /// </param>
        /// <returns>
        /// An array representing the events that are declared or inherited by the <see cref="IntrospectedType"/>,
        /// filtered in accordance to the provided <paramref name="scanOptions"/>.
        /// </returns>
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
        /// Creates an instance of the <see cref="IntrospectedType"/> using the constructor
        /// that best matches the specified parameters via <paramref name="args"/>
        /// </summary>
        /// <param name="args">
        /// An array of arguments that match in count, order and type the parameters
        /// of the constructor to invoke.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="IntrospectedType"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="IntrospectedType"/> is <c><see langword="abstract"/></c> or an interface type.
        /// -OR-
        /// The <see cref="IntrospectedType"/> is a generic definition with unbound type arguments.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        /// The constructor being called throws an exception.
        /// </exception>
        object CreateInstance(params object[] args);

        /// <summary>
        /// The <see cref="Type"/> which the current <see cref="IIntrospector"/> instance provides reflected information for.
        /// </summary>
        Type IntrospectedType { get; }

        /// <summary>
        /// Gets a value representing one or a combination of several <see cref="Axle.Reflection.TypeFlags"/> values which describe
        /// the <see cref="IntrospectedType"/> properties;
        /// </summary>
        /// <seealso cref="Axle.Reflection.TypeFlags"/>
        TypeFlags TypeFlags { get; }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        /// <summary>
        /// Gets the <see cref="AccessModifier"/> for the given type.
        /// </summary>
        AccessModifier AccessModifier { get; }
        #endif

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
        [Obsolete("Check against the ITypeIntrospector.Categories property instead")]
        bool IsGenericType { get; }

        /// <summary>
        /// Checks whether the specified <see cref="IntrospectedType"/> is a generic type definition.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> is a generic type definition; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        [Obsolete("Check against the ITypeIntrospector.Categories property instead")]
        bool IsGenericTypeDefinition { get; }

        /// <summary>
        /// Determines if the <see cref="IntrospectedType"/> is a delegate.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the provided <see cref="IntrospectedType"/> is a delegate;
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        [Obsolete("Check against the ITypeIntrospector.Categories property instead")]
        bool IsDelegate { get; }

        /// <summary>
        /// Checks whether the specified <see cref="IntrospectedType"/> is a nullable type.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> is a nullable type; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        [Obsolete("Check against the ITypeIntrospector.Categories property instead")]
        bool IsNullableType { get; }

        /// <summary>
        /// Checks whether the specified <see cref="IntrospectedType"/> is abstract.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> is abstract; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        [Obsolete("Check against the ITypeIntrospector.Categories property instead")]
        bool IsAbstract { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IntrospectedType"/> represents an enumeration.
        /// </summary>
        /// <returns>
        /// <c><see langword="true"/></c> if the current <see cref="IntrospectedType"/> represents an enumeration; 
        /// <c><see langword="false"/></c> otherwise.
        /// </returns>
        [Obsolete("Check against the ITypeIntrospector.Categories property instead")]
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
