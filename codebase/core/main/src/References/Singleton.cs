#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
using System;
using System.Diagnostics.CodeAnalysis;
#if NETSTANDARD || NET35_OR_NEWER || UNITY_2018_1_OR_NEWER
using System.Linq;
#endif
using System.Reflection;
using System.Runtime.InteropServices;
using Axle.Verification;


namespace Axle.References
{
    /// <summary>
    /// A generic singleton object, exposing a sole instance of the provided type.
    /// According to the singleton design pattern definition, the supplied generic type <typeparamref name="T"/> must
    /// conform to certain requirements, such as to have a <c>private</c> default constructor, and no alternative means
    /// for producing an instance (excluding reflection).
    /// <remarks>
    /// Using the <see cref="Singleton{T}"/> class for obtaining singleton instances provides a guarantee that the
    /// singleton requirements have been met (where an exception will be thrown if the provided type
    /// <typeparamref name="T"/> is not compatible). It also provides an optimisation where the singleton instance is
    /// going to be instantiated only when used.
    /// <para>
    /// However, it may be still possible to obtain multiple instance of the <typeparamref name="T"/> class, if it by
    /// itself implements the singleton pattern in its user code, or if the type's constructor is called via reflection.
    /// </para>
    /// <para>
    /// In order to overcome these limitations, it is encouraged to use the <see cref="ISingletonReference{T}"/> instead
    /// of <typeparamref name="T"/> directly, in order to enforce the singleton semantics. Also, if possible,
    /// user-defined singletons should also implement the <see cref="ISingletonReference{T}"/> interface, thus allowing
    /// for their safe co-existence with <see cref="Singleton{T}"/> singletons.  
    /// </para>
    /// </remarks>
    /// </summary>
    /// <typeparam name="T">
    /// The type of the singleton object instance represented by the current <see cref="Singleton{T}"/> reference.
    /// </typeparam>
    [ComVisible(false)]
    public sealed class Singleton<T> : ISingletonReference<T> where T: class
    {
        internal static T CreateInstance()
        {
            var type = typeof(T);
            var constructor = Singleton.GetSingletonConstructor(type);
            if (constructor == null)
            {
                throw new InvalidOperationException(
                    string.Format(Singleton.CandidateConstructorNotFoundMessageFormat, type.FullName));
            }
            var instance = (T) constructor.Invoke(new object[0]);
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
            System.Threading.Thread.MemoryBarrier();
            #endif
            return instance;
        }

        // The allocator will cause lazy initialization when first accessed by the runtime.
        private static class LazySingletonAllocator
        {
            public static T AllocatedInstance { get; } = CreateInstance();
        }

        [SuppressMessage("ReSharper", "StaticMemberInGenericType")] 
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// Gets the sole instance of the <see cref="Singleton{T}"/> object.
        /// </summary>
        public static Singleton<T> Instance { get; } = new Singleton<T>();

        /// <summary>
        /// Gets the value behind the sole instance of the type <typeparamref name="T"/>.
        /// </summary>
        public T Value => LazySingletonAllocator.AllocatedInstance;

        bool IReference<T>.TryGetValue(out T value)
        {
            value = Value;
            return true;
        }
        T IReference<T>.Value => Value;
        object IReference.Value => Value;
        bool IReference.HasValue => true;

        private Singleton()
        {
            lock (SyncRoot)
            {
                // guarantees single instance
                if (Instance != null)
                {
                    throw new InvalidOperationException(
                        "Cannot create more than one instance of singleton type " + Instance.GetType().FullName);
                }
            }
        }

        /// <summary>
        /// Allows for implicit conversion between a <see cref="Singleton{T}"/> and the underlying type
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <param name="singleton">
        /// The singleton object holding the value.
        /// </param>
        /// <returns>
        /// The underlying value of the <see cref="Singleton{T}"/> object.
        /// </returns>
        public static implicit operator T(Singleton<T> singleton) => singleton.Value;
    }

    /// <summary>
    /// A static class containing utility methods for working with singleton objects.
    /// </summary>
    public static class Singleton
    {
        internal const string CandidateConstructorNotFoundMessageFormat = 
            "The class '{0}' has one or more public constructors and cannot be used with the singleton pattern!";

        internal static ConstructorInfo GetSingletonConstructor(Type type)
        {
            #if NETSTANDARD || UNITY_2018_1_OR_NEWER
            var constructors = type.GetTypeInfo().GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            #else
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            #endif
            #if NET20
            var hasPublicConstructors = false;
            ConstructorInfo constructor = null;;
            for (var i = 0; i < constructors.Length; i++)
            {
                if (constructor == null && constructors[i].GetParameters().Length == 0)
                {
                    constructor = constructors[i];
                }
                if (!hasPublicConstructors && constructors[i].IsPublic)
                {
                    hasPublicConstructors = true;
                }
            }
            if (hasPublicConstructors)
            {
                constructor = null;
            }
            #else
            var hasPublicConstructors = constructors.Any(x => x.IsPublic);
            ConstructorInfo constructor = null;
            if (!hasPublicConstructors)
            {
                constructor = constructors.SingleOrDefault(x => x.GetParameters().Length == 0);
            }
            #endif
            return constructor;
        }

        /// <summary>
        /// Gets a singleton instance for the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        /// The type to create a singleton instance from.
        /// </param>
        /// <returns>
        /// A singleton instance for the provided <paramref name="type"/>.
        /// </returns>
        public static object GetSingletonInstance(Type type)
        {
            var constructor = GetSingletonConstructor(type);
            if (constructor == null)
            {
                // type cannot be instantiated via singleton
                return null;
            }
            var instanceProperty = typeof(Singleton<>)
                .MakeGenericType(type)
                #if NETSTANDARD || UNITY_2018_1_OR_NEWER
                .GetTypeInfo()
                #else
                #endif
                .GetProperty(nameof(Singleton<object>.Instance), BindingFlags.Public|BindingFlags.Static);
            var res = instanceProperty?.GetGetMethod().Invoke(null, null);
            return ((IReference) res)?.Value;
        }

        /// <summary>
        /// Gets a singleton instance for the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        /// The type to create a singleton instance from.
        /// </param>
        /// <returns>
        /// A singleton instance for the provided <paramref name="type"/>.
        /// </returns>
        public static T GetSingletonInstance<T>(Type type) => (T) GetSingletonInstance(type);
        
        /// <summary>
        /// Gets a singleton instance for the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type to create a singleton instance from.
        /// </typeparam>
        /// <returns>
        /// A singleton instance for the provided type <typeparamref name="T"/>.
        /// </returns>
        public static T GetSingletonInstance<T>(ISingletonReference<T> singleton)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(singleton, nameof(singleton)));
            return singleton.Value;
        }
    }
}
#endif