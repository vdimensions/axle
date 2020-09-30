//using System;
//using System.Diagnostics;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
//{
//    public class DependencyResolution : IDependencyResolution
//    {
//        public static DependencyResolution Instance(Type type, object value) => new DependencyResolution(type, value, false, true);
//
//        /// <summary>
//        /// Creates a <see cref="DependencyResolution"/> instance that represents a successful resolution of a prototype object.
//        /// </summary>
//        /// <typeparam name="T">The type of the resolved object.</typeparam>
//        /// <param name="value">The instance of the resolved object</param>
//        /// <returns>
//        /// A <see cref="DependencyResolution"/> instance that represents a successful resolution of a prototype object.
//        /// </returns>
//        public static DependencyResolution Instance<T>(T value) => new DependencyResolution(typeof(T), value, false, true);
//
//        public static DependencyResolution Singleton(Type type, object value) => new DependencyResolution(type, value, true, true);
//
//        /// <summary>
//        /// Creates a <see cref="DependencyResolution"/> instance that represents a successful resolution of a singleton object.
//        /// </summary>
//        /// <typeparam name="T">The type of the resolved object.</typeparam>
//        /// <param name="value">The singleton instance of the resolved object</param>
//        /// <returns>
//        /// A <see cref="DependencyResolution"/> instance that represents a successful resolution of a singleton object.
//        /// </returns>
//        public static DependencyResolution Singleton<T>(T value) { return new DependencyResolution(typeof(T), value, true, true); }
//
//        public static readonly DependencyResolution Failure = new DependencyResolution(typeof (void), null, true, false);
//
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly bool _isSingleton;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly Type _type;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly bool _succeeded;
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly object _value;
//
//        private DependencyResolution(Type type, object value, bool isSingleton, bool succeeded)
//        {
//            _isSingleton = isSingleton;
//            _value = value;
//            _succeeded = succeeded;
//            _type = type;
//        }
//
//        public bool IsSingleton => _isSingleton;
//        public bool Succeeded => _succeeded;
//        public Type Type => _type;
//        public object Value => _value;
//    }
//}