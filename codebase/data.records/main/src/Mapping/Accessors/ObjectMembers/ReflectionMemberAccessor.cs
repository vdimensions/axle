using System;
using System.Linq.Expressions;
using Axle.Reflection;
using Axle.Verification;

namespace Axle.Data.Records.Mapping.Accessors.ObjectMembers
{
    public class ReflectionMemberAccessor<T, TField> : IObjectMemberAccessor<T, TField>
    {
        private readonly IReadWriteMember _targetMember;

        internal ReflectionMemberAccessor(IReadWriteMember accessor)
        {
            _targetMember = accessor;
        }

        public TField GetValue(T obj) => (TField) _targetMember.GetAccessor.GetValue(obj);

        public void SetValue(T obj, TField value) => _targetMember.SetAccessor.SetValue(obj, value);

        public string FieldName => _targetMember.Name;
    }

    public static class ReflectedMemberAccessor
    {
        public static ReflectionMemberAccessor<T, TField> Create<T, TField>(string memberName)
        {
            memberName.VerifyArgument(nameof(memberName)).IsNotNullOrEmpty();
            var introspector = new TypeIntrospector<T>();
            const ScanOptions scanOptions = ScanOptions.Instance | ScanOptions.Public;
            var fieldMember = introspector.GetField(scanOptions, memberName) ??
                              introspector.GetProperty(scanOptions, memberName) as IReadWriteMember;
            return new ReflectionMemberAccessor<T, TField>(fieldMember);
        }

        public static ReflectionMemberAccessor<T, TField> Create<T, TField>(Expression<Func<T, TField>> memberExpr)
        {
            memberExpr.VerifyArgument(nameof(memberExpr)).IsNotNull();
            IReadWriteMember fieldMember = null;
            try
            {
                fieldMember = new TypeIntrospector<T>().GetField(memberExpr);
            }
            catch (ArgumentException)
            {
                fieldMember = new TypeIntrospector<T>().GetProperty(memberExpr) as IReadWriteMember;
                if (fieldMember == null)
                {
                    throw new ArgumentException(
                        "Expression does not represent a valid field or read-write-enabled property.",
                        nameof(memberExpr));
                }
            }

            return new ReflectionMemberAccessor<T, TField>(fieldMember);
        }
    }
}