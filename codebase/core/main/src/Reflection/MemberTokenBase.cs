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
    //[Maturity(CodeMaturity.Stable)]
    public abstract partial class MemberTokenBase<T> : IReflected<T>, IMember, IEquatable<MemberTokenBase<T>> where T: MemberInfo
    {
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type declaringType;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RuntimeTypeHandle typeHandle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string name;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IEnumerable<IAttributeInfo> attributes;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected internal readonly IReadWriteLock Lock = new ReadWriteLock();

        protected MemberTokenBase(T member, Type declaringType, string name)
        {
            this.name = name;
            this.declaringType = declaringType;
            this.typeHandle = declaringType.TypeHandle;
            #if NETSTANDARD
            this.ReflectedMember = member;
            #endif
        }

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

        public IEnumerable<IAttributeInfo> Attributes
        {
            get
            {
                // IMPORTANT! Get the reflected member outside the lock to prevent recursive entrancy!
                var reflectedMember = ReflectedMember;

                return Lock.Invoke(
                    () => attributes,
                    xx => xx == null,
                    () =>
                    {
                        var comparer = EqualityComparer<Attribute>.Default;
                        var notInherited = reflectedMember.GetCustomAttributes(false).Cast<Attribute>();
                        var inherited = reflectedMember.GetCustomAttributes(true).Cast<Attribute>().Except(notInherited, comparer);

                        attributes = notInherited
                            .Select(
                                x => new
                                {
                                    Attribute = x,
                                    Inherited = false,
                                    #if !NETSTANDARD
                                    AttributeUsage = (x.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute), false))
                                    #else
                                    AttributeUsage = (x.GetType().GetTypeInfo().GetCustomAttributes(typeof(AttributeUsageAttribute), false))
                                    #endif
                                    .Cast<AttributeUsageAttribute>()
                                    .Single()
                                })
                            .Union(
                                inherited.Select(
                                    x => new
                                    {
                                        Attribute = x,
                                        Inherited = true,
                                        #if !NETSTANDARD
                                        AttributeUsage = (x.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute), false))
                                        #else
                                        AttributeUsage = (x.GetType().GetTypeInfo().GetCustomAttributes(typeof(AttributeUsageAttribute), false))
                                        #endif
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

        #if !NETSTANDARD
        public abstract T ReflectedMember { get; }
        #else
        public virtual T ReflectedMember { get; }
        #endif
    }

    //[Maturity(CodeMaturity.Stable)]
    public abstract partial class MemberTokenBase<T, THandle> : MemberTokenBase<T>, 
        IEquatable<MemberTokenBase<T, THandle>>
        where T: MemberInfo
        where THandle: struct
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly THandle handle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly WeakRef<T> memberRef;

        protected MemberTokenBase(T member, THandle handle, Type declaringType, string name) : base(member, declaringType, name)
        {
            this.memberRef = new WeakRef<T>(member);
            this.handle = handle;
        }

        public virtual bool Equals(MemberTokenBase<T, THandle> other) { return EqualityComparer.Equals(this, other); }
        public override bool Equals(object obj) { return EqualityComparer.Equals(this, obj); }

        public override int GetHashCode() { return EqualityComparer.GetHashCode(this); }

        protected abstract T GetMember(THandle handle, RuntimeTypeHandle typeHandle, bool isGeneric);

        public THandle Handle => handle;

        public sealed override T ReflectedMember
        {
            get
            {
                T item = null;
                Lock.Invoke(
                    () => item = memberRef.Value,
                    xx => xx == null || !memberRef.IsAlive,
                    #if !NETSTANDARD
                    () => memberRef.Value = item = GetMember(handle, TypeHandle, DeclaringType.IsGenericType));
                    #else
                    () => memberRef.Value = item = GetMember(handle, TypeHandle, DeclaringType.GetTypeInfo().IsGenericType));
                    #endif
                return item;
            }
        }
    }
}