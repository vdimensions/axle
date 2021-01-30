using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using Axle.Reflection.Extensions;
using Axle.Threading;
using Axle.Threading.ReaderWriterLock;
#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using Axle.References;
#endif

namespace Axle.Reflection
{

    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract partial class MemberTokenBase<T> : IReflected<T>, IMember, IEquatable<MemberTokenBase<T>>, IAttributeTarget
        where T: MemberInfo
    {
        public IAttributeInfo[] GetAttributes(T reflectedMember) => CustomAttributeProviderExtensions.GetEffectiveAttributes(reflectedMember) ?? new IAttributeInfo[0];
        public IAttributeInfo[] GetAttributes(T reflectedMember, Type attributeType)
        {
            return CustomAttributeProviderExtensions.GetEffectiveAttributes(reflectedMember, attributeType) ?? new IAttributeInfo[0];
        }
        public IAttributeInfo[] GetAttributes(T reflectedMember, Type attributeType, bool inherit)
        {
            var attrs = Enumerable.Cast<Attribute>(reflectedMember.GetCustomAttributes(attributeType, inherit));
            return inherit
                ? CustomAttributeProviderExtensions.FilterAttributes(new Attribute[0], attrs)
                : CustomAttributeProviderExtensions.FilterAttributes(attrs, new Attribute[0]);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _declaringType;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RuntimeTypeHandle _typeHandle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _name;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        #if NET20
        protected internal readonly ILock Lock = new MonitorLock();
        #else
        protected readonly IReadWriteLock Lock = new ReadWriteLock();
        #endif

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected MemberTokenBase(Type declaringType, string name)
        {
            _name = name;
            _declaringType = declaringType;
            _typeHandle = declaringType.TypeHandle;
        }
        #endif

        protected MemberTokenBase(T member, Type declaringType, string name)
        {
            _name = name;
            _declaringType = declaringType;
            _typeHandle = declaringType.TypeHandle;
            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            #else
            ReflectedMember = member;
            #endif
        }

        public virtual bool Equals(MemberTokenBase<T> other) => EqualityComparer.Equals(this, other);
        public override bool Equals(object obj) => EqualityComparer.Equals(this, obj);

        public IAttributeInfo[] GetAttributes() => GetAttributes(ReflectedMember);
        public IAttributeInfo[] GetAttributes(Type attributeType) => GetAttributes(ReflectedMember, attributeType);
        public IAttributeInfo[] GetAttributes(Type attributeType, bool inherit) => GetAttributes(ReflectedMember, attributeType, inherit);

        public override int GetHashCode() => EqualityComparer.GetHashCode(this);

        public bool IsDefined(Type attributeType, bool inherit) => ReflectedMember.IsDefined(attributeType, inherit);

        MemberInfo IReflected.ReflectedMember => ReflectedMember;
        public Type DeclaringType => _declaringType;
        public abstract Type MemberType { get; }
        public string Name => _name;
        public abstract DeclarationType Declaration { get; }
        public abstract AccessModifier AccessModifier { get; }
        public RuntimeTypeHandle TypeHandle => _typeHandle;

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public abstract T ReflectedMember { get; }
        #else
        public virtual T ReflectedMember { get; }
        #endif
    }

    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    public abstract partial class MemberTokenBase<T, THandle> : MemberTokenBase<T>,
        IEquatable<MemberTokenBase<T, THandle>>
        where T: MemberInfo
        where THandle: struct
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly THandle _handle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly WeakRef<T> _memberRef;

        protected MemberTokenBase(T member, THandle handle, Type declaringType, string name) : base(member, declaringType, name)
        {
            _memberRef = new WeakRef<T>(member);
            _handle = handle;
        }

        public virtual bool Equals(MemberTokenBase<T, THandle> other) => EqualityComparer.Equals(this, other);
        public override bool Equals(object obj) => EqualityComparer.Equals(this, obj);

        public override int GetHashCode() => EqualityComparer.GetHashCode(this);

        protected abstract T GetMember(THandle handle, RuntimeTypeHandle typeHandle, bool isGeneric);

        public THandle Handle => _handle;

        public sealed override T ReflectedMember
        {
            get
            {
                T item = null;
                #if NET20
                return LockExtensions.Invoke(
                #else
                return ReaderWriterLockExtensions.Invoke(
                #endif
                    Lock,
                    () => item = _memberRef.Value,
                    xx => xx == null || !_memberRef.IsAlive,
                    #if NETSTANDARD
                    () => _memberRef.Value = item = GetMember(_handle, TypeHandle, DeclaringType.GetTypeInfo().IsGenericType));
                    #else
                    () => _memberRef.Value = item = GetMember(_handle, TypeHandle, DeclaringType.IsGenericType));
                    #endif
            }
        }
    }
    #endif
}