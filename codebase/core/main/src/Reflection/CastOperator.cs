#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Axle.References;


namespace Axle.Reflection
{
    /// <summary>
    /// A static class for handling cast operator; that is, an operator defined on a type that allows instances of that type
    /// to be converted to instances of another type.
    /// </summary>
    /// <seealso cref="ICastOperator{T1, T2}"/>
    public static class CastOperator
    {
        internal sealed class CastOp<T1, T2> : ICastOperator<T1, T2>
        {
            private abstract class BaseCastOperator : ICastOperator<T1, T2>
            {
                [DebuggerBrowsable(DebuggerBrowsableState.Never)]
                private readonly MethodInfo method;

                public bool IsDefined => method != null;

                protected BaseCastOperator(string operatorName)
                {
                    const BindingFlags flags = BindingFlags.Static | BindingFlags.Public;
                    method = FindMethod(typeof(T1), operatorName, typeof(T2), flags);
                }

                private object Invoke(object target)
                {
                    if (!IsDefined)
                    {
                        return new InvalidCastException(string.Format("No cast operator is defined for types {0} and {1}.",
                            typeof(T1).FullName,
                            typeof(T2).FullName));
                    }
                    return (T2) method.Invoke(null, new[] { target });
                }

                object ICastOperator.Invoke(object target) { return Invoke(target); }
                T2 ICastOperator<T1, T2>.Invoke(T1 target) { return (T2) Invoke(target); }

                bool ICastOperator.TryInvoke(object target, out object result)
                {
                    result = null;
                    if (!IsDefined)
                    {
                        return false;
                    }
                    try
                    {
                        result = Invoke(target);
                        return true;
                    }
                    catch (Exception)
                    {
                        result = null;
                        return false;
                    }
                }
                bool ICastOperator<T1, T2>.TryInvoke(T1 target, out T2 result)
                {
                    result = default(T2);
                    if (!IsDefined)
                    {
                        return false;
                    }
                    try
                    {
                        result = (T2) Invoke(target);
                        return true;
                    }
                    catch (Exception)
                    {
                        result = default(T2);
                        return false;
                    }
                }
            }
            private sealed class ExplicitCastOperator : BaseCastOperator
            {
                private ExplicitCastOperator() : base("op_Explicit") { }
            }
            private sealed class ImplicitCastOperator : BaseCastOperator
            {
                private ImplicitCastOperator() : base("op_Implicit") { }
            }

            public static CastOp<T1, T2> Instance => Singleton<CastOp<T1, T2>>.Instance;

            private CastOp() {}

            private static MethodInfo FindMethod(Type targetType, string methodName, Type returnType, BindingFlags bindingFlags)
            {
                #if NETSTANDARD1_5_OR_NEWER || NET45_OR_NEWER
                var methods = targetType.GetTypeInfo().GetMethods(bindingFlags);
                #else
                var methods = targetType.GetMethods(bindingFlags);
                #endif
                return Enumerable.SingleOrDefault(methods, m => m.Name.Equals(methodName, StringComparison.Ordinal) && m.ReturnType.Equals(returnType));
            }

            public T2 Invoke(T1 target) { return (T2) DoInvoke(target); }
            public object Invoke(object target) { return DoInvoke(target); }

            public bool TryInvoke(T1 target, out T2 result)
            {
                foreach (var op in new[]{ImplicitOperator, ExplicitOperator})
                {
                    if (op.IsDefined)
                    {
                        return op.TryInvoke(target, out result);
                    }
                }
                result = default(T2);
                return false;
            }
            public bool TryInvoke(object target, out object result)
            {
                foreach (var op in new[] { ImplicitOperator, ExplicitOperator })
                {
                    if (op.IsDefined)
                    {
                        return op.TryInvoke(target, out result);
                    }
                }
                result = default(T2);
                return false;
            }

            private object DoInvoke(object target)
            {
                var castOperator = Enumerable.FirstOrDefault(new[] { ImplicitOperator, ExplicitOperator }, x => x.IsDefined);
                if (castOperator != null)
                {
                    return castOperator.Invoke(target);
                }
                throw new InvalidCastException(string.Format("{0} does not define any implicit or explicit casts to type {1}", typeof(T1).FullName, typeof(T2).FullName));
            }

            bool ICastOperator.IsDefined => ImplicitOperator.IsDefined || ExplicitOperator.IsDefined;

            private ICastOperator<T1, T2> ImplicitOperator => Singleton<ImplicitCastOperator>.Instance.Value;
            private ICastOperator<T1, T2> ExplicitOperator => Singleton<ExplicitCastOperator>.Instance.Value;
        }

        public static ICastOperator For(Type source, Type target)
        {
            return Singleton.GetSingletonInstance<ICastOperator>(typeof(CastOp<,>).MakeGenericType(source, target));
        }
        public static ICastOperator<T, TResult> For<T, TResult>() => CastOp<T, TResult>.Instance;

        public static TResult Cast<T, TResult>(
            #if NETSTANDARD || NET35_OR_NEWER
            this
            #endif
            T target) => For<T, TResult>().Invoke(target);
    }
}
#endif
