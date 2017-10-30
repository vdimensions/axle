using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Threading.ReaderWriterLock;


namespace Axle.Reflection
{
    [Serializable]
    partial class MemberTokenBase<T>
    {
        protected MemberTokenBase(Type declaringType, string name)
        {
            this.name = name;
            this.declaringType = declaringType;
            this.typeHandle = declaringType.TypeHandle;
        }

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

        public abstract T ReflectedMember { get; }
    }

    [Serializable]
    partial class MemberTokenBase<T, THandle>
    {
        protected MemberTokenBase(T member, THandle handle, Type declaringType, string name) : base(declaringType, name)
        {
            this.memberRef = new Axle.References.WeakReference<T>(member);
            this.handle = handle;
        }

        public sealed override T ReflectedMember
        {
            get
            {
                T item = null;
                Lock.Invoke(
                    () => item = memberRef.Value,
                    xx => xx == null || !memberRef.IsAlive,
                    () => memberRef.Value = item = GetMember(handle, TypeHandle, DeclaringType.IsGenericType));
                return item;
            }
        }
    }
}