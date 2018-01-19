using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Axle.Threading.ReaderWriterLock;


namespace Axle.Reflection
{
    partial class MemberTokenBase<T>
    {
        protected MemberTokenBase(T member, Type declaringType, string name)
        {
            this.name = name;
            this.declaringType = declaringType;
            this.typeHandle = declaringType.TypeHandle;
            this.ReflectedMember = member;
        }

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
                                    AttributeUsage = (x.GetType().GetTypeInfo().GetCustomAttributes(typeof(AttributeUsageAttribute), false))
                                    .Cast<AttributeUsageAttribute>()
                                    .Single()
                                })
                            .Union(
                                inherited.Select(
                                    x => new
                                    {
                                        Attribute = x,
                                        Inherited = true,
                                        AttributeUsage = (x.GetType().GetTypeInfo().GetCustomAttributes(typeof(AttributeUsageAttribute), false))
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

        public virtual T ReflectedMember { get; }
    }

    partial class MemberTokenBase<T, THandle>
    {
        protected MemberTokenBase(T member, THandle handle, Type declaringType, string name) : base(member, declaringType, name)
        {
            this.memberRef = new Axle.References.WeakReference<T>(member);
            this.handle = handle;
        }

        public override T ReflectedMember
        {
            get
            {
                T item = null;
                Lock.Invoke(
                    () => item = memberRef.Value,
                    xx => xx == null || !memberRef.IsAlive,
                    () => memberRef.Value = item = GetMember(handle, TypeHandle, DeclaringType.GetTypeInfo().IsGenericType));
                return item;
            }
        }
    }
}