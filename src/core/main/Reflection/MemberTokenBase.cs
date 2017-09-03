using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using Axle.References;
using Axle.Threading;


namespace Axle.Reflection
{
    //[Maturity(CodeMaturity.Stable)]
    public abstract partial class MemberTokenBase<T> : IReflected<T>, IMember, IEquatable<MemberTokenBase<T>> where T: MemberInfo
    {
#if !netstandard
        [Serializable]
#endif
        protected struct AttributeInfo : IAttributeInfo
        {
            public Attribute Attribute { get; internal set; }
            public AttributeTargets AttributeTargets { get; internal set; }
            public bool AllowMultiple { get; internal set; }
            public bool Inherited { get; internal set; }
        }

        protected internal static AccessModifier GetAccessModifier(bool isPublic, bool isAssembly, bool isFamily, bool isPrivate)
        {
            if (isPublic)
            {
                return AccessModifier.Public;
            }
            if (isPrivate)
            {
                return AccessModifier.Private;
            }
            if (isFamily && !isAssembly)
            {
                return AccessModifier.Protected;
            }
            if (isAssembly && !isFamily)
            {
                return AccessModifier.Internal;
            }
            return AccessModifier.ProtectedInternal;
        }

#if !netstandard
        [Serializable]
#endif
        private class MethodHandleBaseEqualityComparer<TT> : AbstractEqualityComparer<TT> where TT: MemberTokenBase<T>
        {
            internal MethodHandleBaseEqualityComparer() { }

            protected override int DoGetHashCode(TT obj) { return (obj.ReflectedMember.GetHashCode()); }

            protected override bool DoEquals(TT x, TT y) { return x.ReflectedMember.Equals(y.ReflectedMember); }
        }

#if !netstandard
        private static AbstractEqualityComparer<MemberTokenBase<T>> EqualityComparer => Singleton<MethodHandleBaseEqualityComparer<MemberTokenBase<T>>>.Instance;
#else
        private static AbstractEqualityComparer<MemberTokenBase<T>> comparer = new MethodHandleBaseEqualityComparer<MemberTokenBase<T>>();
        private static AbstractEqualityComparer<MemberTokenBase<T>> EqualityComparer => comparer;
#endif

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type declaringType;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RuntimeTypeHandle typeHandle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string name;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IEnumerable<IAttributeInfo> attributes;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected internal readonly IReentrantReadWriteLock Lock = new ReentrantReadWriteLock();

        public virtual bool Equals(MemberTokenBase<T> other) { return EqualityComparer.Equals(this, other); }
        public override bool Equals(object obj) { return EqualityComparer.Equals(this, obj); }

        public override int GetHashCode() { return EqualityComparer.GetHashCode(this); }

        MemberInfo IReflected.ReflectedMember => ReflectedMember;
        public Type DeclaringType => declaringType;
        public abstract Type MemberType { get; }
        public string Name => name;
        public abstract DeclarationType Declaration { get; }
        public abstract AccessModifier AccessModifier { get; }
        public RuntimeTypeHandle TypeHandle => typeHandle;
    }

    //[Maturity(CodeMaturity.Stable)]
    public abstract partial class MemberTokenBase<T, THandle> : MemberTokenBase<T>, 
        IEquatable<MemberTokenBase<T, THandle>>
        where T: MemberInfo
        where THandle: struct
    {
#if !netstandard
        [Serializable]
#endif
        private sealed class MethodHandleBaseEqualityComparer : AbstractEqualityComparer<MemberTokenBase<T, THandle>>
        {
            internal MethodHandleBaseEqualityComparer() { }

            protected override int DoGetHashCode(MemberTokenBase<T, THandle> obj)
            {
                return (obj.Handle.GetHashCode());
            }

            protected override bool DoEquals(MemberTokenBase<T, THandle> x, MemberTokenBase<T, THandle> y)
            {
                return x.Handle.Equals(y.Handle);
            }
        }
#if !netstandard
        private static AbstractEqualityComparer<MemberTokenBase<T, THandle>> EqualityComparer
        {
            get { return Singleton<MethodHandleBaseEqualityComparer>.Instance; }
        }
#else
        private static readonly AbstractEqualityComparer<MemberTokenBase<T, THandle>> comparer = new MethodHandleBaseEqualityComparer();
        private static AbstractEqualityComparer<MemberTokenBase<T, THandle>> EqualityComparer
        {
            get { return comparer; }
        }
#endif

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly THandle handle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Axle.References.WeakReference<T> memberRef;

        public virtual bool Equals(MemberTokenBase<T, THandle> other) { return EqualityComparer.Equals(this, other); }
        public override bool Equals(object obj) { return EqualityComparer.Equals(this, obj); }

        public override int GetHashCode() { return EqualityComparer.GetHashCode(this); }

        protected abstract T GetMember(THandle handle, RuntimeTypeHandle typeHandle, bool isGeneric);

        public THandle Handle { get { return handle; } }
    }
}