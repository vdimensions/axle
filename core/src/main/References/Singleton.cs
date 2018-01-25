#if NETSTANDARD1_0 ||  NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4
#else
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Axle.References
{
    [ComVisible(false)]
    public sealed partial class Singleton<T> : ISingetonReference<T> where T: class
    {
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

    public static partial class Singleton
    {
        internal const string CandidateConstructorNotFoundMessageFormat = "The class '{0}' has one or more public constructors and cannot be used with the singleton pattern!";
        
        public static T GetSingletonInstance<T>(Type type) { return (T) GetSingletonInstance(type); }
    }
}
#endif