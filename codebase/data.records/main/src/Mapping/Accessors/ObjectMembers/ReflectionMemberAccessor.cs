using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Axle.Reflection;
using Axle.Verification;

namespace Axle.Data.Records.Mapping.Accessors.ObjectMembers
{
    public static class ReflectedMemberAccessor
    {
        private sealed class Direct<T, TField> : IObjectMemberAccessor<T, TField>
        {
            public static Direct<T, TField> CreateInstance(IReadWriteMember accessor)
            {
                return new Direct<T, TField>(accessor);
            }

            private readonly IReadWriteMember _targetMember;

            private Direct(IReadWriteMember accessor)
            {
                _targetMember = accessor;
            }

            public TField GetValue(T obj) => (TField) _targetMember.GetAccessor.GetValue(obj);

            public void SetValue(T obj, TField value) => _targetMember.SetAccessor.SetValue(obj, value);

            public Direct<T, TResult> ChangeReturnType<TResult>() => Direct<T, TResult>.CreateInstance(_targetMember);

            public string FieldName => _targetMember.Name;
        }

        private sealed class Indirect<T, TIntermediate, TField> : IObjectMemberAccessor<T, TField>
        {
            public static Indirect<T, TIntermediate, TField> CreateInstance(
                IObjectMemberAccessor<T, TIntermediate> i1, 
                Direct<TIntermediate, TField> i2)
            {
                return new Indirect<T, TIntermediate, TField>(i1, i2);
            }

            private readonly IObjectMemberAccessor<T, TIntermediate> _i1;
            private readonly Direct<TIntermediate, TField> _i2;

            private Indirect(IObjectMemberAccessor<T, TIntermediate> i1, Direct<TIntermediate, TField> i2)
            {
                _i1 = i1;
                _i2 = i2;
            }

            public TField GetValue(T obj) => _i2.GetValue(_i1.GetValue(obj));

            public void SetValue(T obj, TField value) => _i2.SetValue(_i1.GetValue(obj), value);

            public Indirect<T, TIntermediate, TResult> ChangeReturnType<TResult>() 
                => Indirect<T, TIntermediate, TResult>.CreateInstance(_i1, _i2.ChangeReturnType<TResult>());

            public string FieldName => _i2.FieldName;
        }

        private static IObjectMemberAccessor<T, TResult> ChangeReturnType<T, TResult>(
            IObjectMemberAccessor<T, object> accessor)
        {
            switch (accessor)
            {
                case Direct<T, object> d:
                    return d.ChangeReturnType<TResult>();
                case Indirect<T, object, object> indirect:
                    return indirect.ChangeReturnType<TResult>();
                default:
                    throw new InvalidOperationException();
            }
        }

        private static IReadWriteMember GetMember(MemberExpression expression)
        {
            var member = expression.Member;
            var typeIntrospector = new TypeIntrospector(member.DeclaringType);
            var fieldMember = member is FieldInfo fi
                ? typeIntrospector.GetField(fi)
                : member is PropertyInfo pi
                    ? typeIntrospector.GetProperty(pi) as IReadWriteMember
                    : null;
            return fieldMember;
        }
        
        public static IEnumerable<MemberExpression> ExpandMemberChain(Expression expression)
        {
            switch (expression)
            {
                case MemberExpression memberExpression:
                    yield return memberExpression;
                    break;
                case LambdaExpression lambdaExpression:
                    foreach (var bodyExpression in ExpandMemberChain(lambdaExpression.Body))
                    {
                        yield return bodyExpression;
                    }
                    break;
                case UnaryExpression unaryExpression:
                    foreach (var bodyExpression in ExpandMemberChain(unaryExpression.Operand))
                    {
                        yield return bodyExpression;
                    }
                    break;
            }
        }
        
        public static IObjectMemberAccessor<T, TField> Create<T, TField>(string memberName)
        {
            memberName.VerifyArgument(nameof(memberName)).IsNotNullOrEmpty();
            var introspector = new TypeIntrospector<T>();
            const ScanOptions scanOptions = ScanOptions.Instance | ScanOptions.Public;
            var fieldMember = introspector.GetField(scanOptions, memberName) ??
                              introspector.GetProperty(scanOptions, memberName) as IReadWriteMember;
            return Direct<T, TField>.CreateInstance(fieldMember);
        }

        public static IObjectMemberAccessor<T, TField> Create<T, TField>(Expression<Func<T, TField>> memberExpr)
        {
            memberExpr.VerifyArgument(nameof(memberExpr)).IsNotNull();
            IObjectMemberAccessor<T, object> tmp = null;
            var expandedMembers = ExpandMemberChain(memberExpr).ToArray();
            if (expandedMembers.Length >= 1)
            {
                var fieldMember = GetMember(expandedMembers[0]);
                if (fieldMember == null)
                {
                    throw new ArgumentException(
                        "Expression does not represent a valid field or read-write-enabled property.",
                        nameof(memberExpr));
                }
                tmp = Direct<T, object>.CreateInstance(fieldMember);
            }
            else
            {
                throw new ArgumentException(
                    "Expression does not represent a valid field or read-write-enabled property.",
                    nameof(memberExpr));
            }

            for (var i = 1; i < expandedMembers.Length; i++)
            {
                var fieldMember = GetMember(expandedMembers[i]);
                if (fieldMember == null)
                {
                    throw new ArgumentException(
                        "Expression does not represent a valid field or read-write-enabled property.",
                        nameof(memberExpr));
                }
                tmp = Indirect<T, object, object>.CreateInstance(tmp, Direct<object, object>.CreateInstance(fieldMember));
            }
            return ChangeReturnType<T, TField>(tmp);
        }
    }
}