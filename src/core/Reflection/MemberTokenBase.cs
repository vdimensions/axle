using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Axle.References;
using Axle.Threading;
using Axle.Threading.ReaderWriterLock;


namespace Axle.Reflection
{
    [Serializable]
    //[Maturity(CodeMaturity.Stable)]
    public abstract class MemberTokenBase<T> : IReflected<T>, IMember, IEquatable<MemberTokenBase<T>> where T: MemberInfo
    {
        [Serializable]
        protected struct AttributeInfo : IAttributeInfo
        {
            public Attribute Attribute { get; internal set; }
            public AttributeTargets AttributeTargets { get; internal set; }
            public bool AllowMultiple { get; internal set; }
            public bool Inherited { get; internal set; }
        }

        internal static AccessModifier GetAccessModifier(bool isPublic, bool isAssembly, bool isFamily, bool isPrivate)
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

        [Serializable]
        private class MethodHandleBaseEqualityComparer<TT> : AbstractEqualityComparer<TT> where TT: MemberTokenBase<T>
        {
            private MethodHandleBaseEqualityComparer() { }

            protected override int DoGetHashCode(TT obj) { return (obj.ReflectedMember.GetHashCode()); }

            protected override bool DoEquals(TT x, TT y) { return x.ReflectedMember.Equals(y.ReflectedMember); }
        }

        private static AbstractEqualityComparer<MemberTokenBase<T>> EqualityComparer
        {
            get { return Singleton<MethodHandleBaseEqualityComparer<MemberTokenBase<T>>>.Instance; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type declaringType;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string name;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IEnumerable<IAttributeInfo> attributes;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected internal readonly IReentrantReadWriteLock Lock = new ReentrantReadWriteLock();

        protected MemberTokenBase(Type declaringType, string name)
        {
            this.name = name;
            this.declaringType = declaringType;
        }

        public virtual bool Equals(MemberTokenBase<T> other) { return EqualityComparer.Equals(this, other); }
        public override bool Equals(object obj) { return EqualityComparer.Equals(this, obj); }

        public override int GetHashCode() { return EqualityComparer.GetHashCode(this); }

        public abstract T ReflectedMember { get; }
        MemberInfo IReflected.ReflectedMember { get { return this.ReflectedMember; } }
        public Type DeclaringType { get { return declaringType; } }
        public abstract Type MemberType { get; }
        public string Name { get { return name; } }
        public abstract DeclarationType Declaration { get; }
        public abstract AccessModifier AccessModifier { get; }
        public virtual IEnumerable<IAttributeInfo> Attributes
        {
            get
            {
                return Lock.Invoke(
                    () => attributes,
                    xx => xx == null,
                    () =>
                    {
                        var comparer = EqualityComparer<Attribute>.Default;
                        var notInherited = ReflectedMember.GetCustomAttributes(false).Cast<Attribute>();
                        var inherited = ReflectedMember.GetCustomAttributes(true).Cast<Attribute>().Except(notInherited, comparer);

                        attributes = notInherited
                            .Select(
                                x => new
                                {
                                    Attribute = x,
                                    Inherited = false,
                                    AttributeUsage = (x.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute), false))
                                        .Cast<AttributeUsageAttribute>()
                                        .Single()
                                })
                            .Union(
                                inherited.Select(
                                    x => new
                                    {
                                        Attribute = x,
                                        Inherited = true,
                                        AttributeUsage = (x.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute), false))
                                            .Cast<AttributeUsageAttribute>()
                                            .Single()
                                    }))
                            .Select(
                                x => new AttributeInfo
                                {
                                    Attribute = x.Attribute,
                                    AllowMultiple = x.AttributeUsage.AllowMultiple,
                                    AttributeTargets = x.AttributeUsage.ValidOn,
                                    Inherited = x.Inherited
                                } as IAttributeInfo);
                        return attributes.ToArray();
                    }) ?? new IAttributeInfo[0];
            }
        }
    }

    [Serializable]
    //[Maturity(CodeMaturity.Stable)]
    public abstract class MemberTokenBase<T, THandle> : MemberTokenBase<T>, 
        IEquatable<MemberTokenBase<T, THandle>>
        where T: MemberInfo
        where THandle: struct
    {
        [Serializable]
        private sealed class MethodHandleBaseEqualityComparer : AbstractEqualityComparer<MemberTokenBase<T, THandle>>
        {
            private MethodHandleBaseEqualityComparer() { }

            protected override int DoGetHashCode(MemberTokenBase<T, THandle> obj)
            {
                return (obj.Handle.GetHashCode());
            }

            protected override bool DoEquals(MemberTokenBase<T, THandle> x, MemberTokenBase<T, THandle> y)
            {
                return x.Handle.Equals(y.Handle);
            }
        }

        private static AbstractEqualityComparer<MemberTokenBase<T, THandle>> EqualityComparer
        {
            get { return Singleton<MethodHandleBaseEqualityComparer>.Instance; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly THandle handle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RuntimeTypeHandle typeHandle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly WeakReference<T> memberRef;

        protected MemberTokenBase(T member, THandle handle, Type declaringType, string name) : base(declaringType, name)
        {
            this.memberRef = new WeakReference<T>(member);
            this.handle = handle;
            this.typeHandle = declaringType.TypeHandle;
        }

        public virtual bool Equals(MemberTokenBase<T, THandle> other) { return EqualityComparer.Equals(this, other); }
        public override bool Equals(object obj) { return EqualityComparer.Equals(this, obj); }

        public override int GetHashCode() { return EqualityComparer.GetHashCode(this); }

        protected abstract T GetMember(THandle handle, RuntimeTypeHandle typeHandle, bool isGeneric);

        public THandle Handle { get { return handle; } }
        public RuntimeTypeHandle TypeHandle { get { return typeHandle; } }
        public override T ReflectedMember
        {
            get
            {
                T item = null;
                Lock.Invoke(
                    () => item = memberRef.Value,
                    xx => xx == null || !memberRef.IsAlive,
                    () => memberRef.Value = item = GetMember(handle, typeHandle, DeclaringType.IsGenericType));
                return item;
            }
        }
    }
}