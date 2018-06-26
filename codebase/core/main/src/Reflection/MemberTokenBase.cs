using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using Axle.References;
using Axle.Reflection.Extensions;
using Axle.Threading;
using Axle.Threading.ReaderWriterLock;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
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
        private readonly Type _declaringType;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RuntimeTypeHandle _typeHandle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _name;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IEnumerable<IAttributeInfo> _attributes;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected internal readonly IReadWriteLock Lock = new ReadWriteLock();

        #if !NETSTANDARD
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
            #if NETSTANDARD
            ReflectedMember = member;
            #endif
        }

        public virtual bool Equals(MemberTokenBase<T> other) { return EqualityComparer.Equals(this, other); }
        public override bool Equals(object obj) { return EqualityComparer.Equals(this, obj); }

        public override int GetHashCode() { return EqualityComparer.GetHashCode(this); }

        MemberInfo IReflected.ReflectedMember => ReflectedMember;
        public Type DeclaringType => _declaringType;
        public abstract Type MemberType { get; }
        public string Name => _name;
        public abstract DeclarationType Declaration { get; }
        public abstract AccessModifier AccessModifier { get; }
        public RuntimeTypeHandle TypeHandle => _typeHandle;

        public IEnumerable<IAttributeInfo> Attributes
        {
            get
            {
                // IMPORTANT! Get the reflected member outside the lock to prevent recursive entrance!
                var reflectedMember = ReflectedMember;

                return Lock.Invoke(
                    () => _attributes,
                    xx => xx == null,
                    () => _attributes = reflectedMember.GetEffectiveAttributes()) ?? new IAttributeInfo[0];
            }
        }

        #if NETSTANDARD
        public virtual T ReflectedMember { get; }
        #else
        public abstract T ReflectedMember { get; }
        #endif
    }

    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
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

        public virtual bool Equals(MemberTokenBase<T, THandle> other) { return EqualityComparer.Equals(this, other); }
        public override bool Equals(object obj) { return EqualityComparer.Equals(this, obj); }

        public override int GetHashCode() { return EqualityComparer.GetHashCode(this); }

        protected abstract T GetMember(THandle handle, RuntimeTypeHandle typeHandle, bool isGeneric);

        public THandle Handle => _handle;

        public sealed override T ReflectedMember
        {
            get
            {
                T item = null;
                Lock.Invoke(
                    () => item = _memberRef.Value,
                    xx => xx == null || !_memberRef.IsAlive,
                    #if NETSTANDARD || NET45_OR_NEWER
                    () => _memberRef.Value = item = GetMember(_handle, TypeHandle, DeclaringType.GetTypeInfo().IsGenericType));
                    #else
                    () => _memberRef.Value = item = GetMember(_handle, TypeHandle, DeclaringType.IsGenericType));
                    #endif
                return item;
            }
        }
    }
}