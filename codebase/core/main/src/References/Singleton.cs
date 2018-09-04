#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;


namespace Axle.References
{
    [ComVisible(false)]
    public sealed class Singleton<T> : ISingetonReference<T> where T: class
    {
        internal static T CreateInstance()
        {
            var type = typeof(T);
            var constructor = Singleton.GetSingletonConstructor(type);
            if (constructor == null)
            {
                throw new InvalidOperationException(string.Format(Singleton.CandidateConstructorNotFoundMessageFormat, type.FullName));
            }
            var instance = (T) constructor.Invoke(new object[0]);
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            System.Threading.Thread.MemoryBarrier();
            #endif
            return instance;
        }

        // The allocator will cause lazy initialization when first accessed by the runtime.
        private static class LazySingletonAllocator
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private static readonly T _instance = CreateInstance();
            public static T Instance => _instance;
        }

        private static readonly object _syncRoot = new object();
        private static readonly Singleton<T> _instance = new Singleton<T>();
        public static Singleton<T> Instance => _instance;

        public T Value => LazySingletonAllocator.Instance;
        T IReference<T>.Value => Value;
        object IReference.Value => Value;

        private Singleton()
        {
            lock (_syncRoot)
            {
                // guarantees single instance
                if (_instance != null)
                {
                    throw new InvalidOperationException("Cannot create more than one instance of singleton type " + _instance.GetType().FullName);
                }
            }
        }

        public static implicit operator T(Singleton<T> singleton) { return singleton.Value; }
    }

    public static class Singleton
    {
        internal const string CandidateConstructorNotFoundMessageFormat = "The class '{0}' has one or more public constructors and cannot be used with the singleton pattern!";
        
        internal static ConstructorInfo GetSingletonConstructor(Type type)
        {
            #if NETSTANDARD || NET45_OR_NEWER
            var constructors = type.GetTypeInfo().GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            #else
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            #endif
            var hasPublicConstructors = constructors.Any(x => x.IsPublic);
            ConstructorInfo constructor = null;
            if (!hasPublicConstructors)
            {
                constructor = constructors.Where(x => x.GetParameters().Length == 0).SingleOrDefault();
            }
            return constructor;
        }

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
                #if NETSTANDARD || NET45_OR_NEWER
                .GetTypeInfo()
                #else
                #endif
                .GetProperty(nameof(Singleton<object>.Instance), BindingFlags.Public|BindingFlags.Static);
            var res = instanceProperty?.GetGetMethod().Invoke(null, null);
            return ((IReference) res)?.Value;
        }

        public static T GetSingletonInstance<T>(Type type) { return (T) GetSingletonInstance(type); }
    }
}
#endif