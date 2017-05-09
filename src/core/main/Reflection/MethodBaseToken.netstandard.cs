using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Axle.Verification;


namespace Axle.Reflection
{
    public abstract partial class MethodBaseToken<T> : MemberTokenBase<T> where T: MethodBase
    {
        internal sealed partial class Parameter
        {

            public Parameter(ParameterInfo parameterInfo)
            {
                this.parameterInfo = parameterInfo.VerifyArgument("parameterInfo").IsNotNull();
                var comparer = EqualityComparer<Attribute>.Default;
                var notInherited = ReflectedMember.GetCustomAttributes(false).Cast<Attribute>();
                var inherited = ReflectedMember.GetCustomAttributes(true).Cast<Attribute>().Except(notInherited, comparer);

                var attr = notInherited
                    .Select(
                        x => new
                        {
                            Attribute = x,
                            Inherited = false,
                            AttributeUsage = (x.GetType().GetTypeInfo().GetCustomAttributes(typeof(AttributeUsageAttribute), false)).Cast<AttributeUsageAttribute>().Single()
                        })
                    .Union(
                        inherited.Select(
                            x => new
                            {
                                Attribute = x,
                                Inherited = true,
                                AttributeUsage = (x.GetType().GetTypeInfo().GetCustomAttributes(typeof(AttributeUsageAttribute), false)).Cast<AttributeUsageAttribute>().Single()
                            }))
                    .Select(
                        x => new AttributeInfo
                        {
                            Attribute = x.Attribute,
                            AllowMultiple = x.AttributeUsage.AllowMultiple,
                            AttributeTargets = x.AttributeUsage.ValidOn,
                            Inherited = x.Inherited
                        } as IAttributeInfo);
                this.attributes = attr.ToArray();

                if (parameterInfo.IsIn)
                {
                    direction &= ParameterDirection.Input;
                }
                if (parameterInfo.IsOut)
                {
                    direction &= ParameterDirection.Output;
                }
                if (parameterInfo.IsRetval)
                {
                    direction &= ParameterDirection.ReturnValue;
                }
            }
        }

        protected internal MethodBaseToken(T info) : base(info, info.DeclaringType, info.Name)
        {
            accessModifier = GetAccessModifier(info.VerifyArgument(nameof(info)).IsNotNull());
            declaration = info.GetDeclarationType();
        }
    }
}