using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;


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
                throw new InvalidOperationException("The class '" + type.FullName + "' has one or more public constructors and cannot be used with the singleton pattern!");
            }
            var instance = (T) constructor.Invoke(new object[0]);
            Thread.MemoryBarrier();
            return instance;
        }

        // The allocator will cause lazy initialization when first accessed by the runtime.
        private static class LazySingletonAllocator
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private static readonly T _instance = CreateInstance();
            public static T Instance { get { return _instance; } }
        }

        private static readonly object _syncRoot = new object();
        private static readonly Singleton<T> _instance = new Singleton<T>();
        public static Singleton<T> Instance { get { return _instance; } }

        public T Value { get { return LazySingletonAllocator.Instance; } }
        T IReference<T>.Value { get { return Value; } }
        object IReference.Value { get { return Value; } }

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
        internal static ConstructorInfo GetSingletonConstructor(Type type)
        {
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
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
                .GetProperty("Instance", BindingFlags.Public|BindingFlags.Static);
            var res = instanceProperty.GetGetMethod().Invoke(null, null);
            return res != null ? ((IReference) res).Value : null;
        }
        public static T GetSingletonInstance<T>(Type type) { return (T) GetSingletonInstance(type); }
    }
}