using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;


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

    public static partial class Singleton
    {
        public static T GetSingletonInstance<T>(Type type) { return (T) GetSingletonInstance(type); }
    }
}