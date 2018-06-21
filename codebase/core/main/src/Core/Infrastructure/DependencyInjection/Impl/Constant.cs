//using System;
//using System.Diagnostics;
//#if NETSTANDARD || NET45_OR_NEWER
//using System.Reflection;
//#endif
//
//using Axle.Reflection.Extensions.Type;
//
//
//namespace Axle.Core.Infrastructure.DependencyInjection.Impl
//{
//    [Obsolete]
//    internal sealed class Constant<T>
//    {
//        public static implicit operator T (Constant<T> constant)
//        {
//            if (constant == null)
//            {
//                #if NETSTANDARD || NET45_OR_NEWER
//                if (typeof(T).GetTypeInfo().IsValueType && !typeof(T).IsNullableType())
//                #else
//                if (typeof(T).IsValueType && !typeof(T).IsNullableType())
//                #endif
//                {
//                    throw new InvalidOperationException("Cannot cast `null` to value type.");
//                }
//                return (T) (object) null;
//            }
//            return constant._value;
//        }
//
//        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//        private readonly T _value;
//
//        public Constant(T value)
//        {
//            _value = value;
//        }
//
//        public T Value => _value;
//    }
//}
