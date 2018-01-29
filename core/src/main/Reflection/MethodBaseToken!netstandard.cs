using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Axle.Verification;


namespace Axle.Reflection
{
    [Serializable]
    public abstract partial class MethodBaseToken<T> : MemberTokenBase<T, RuntimeMethodHandle> where T: MethodBase
    {
        internal sealed partial class Parameter
        {
            public Parameter(ParameterInfo parameterInfo)
            {
                _parameterInfo = parameterInfo.VerifyArgument(nameof(parameterInfo)).IsNotNull();
                var comparer = EqualityComparer<Attribute>.Default;
                var notInherited = ReflectedMember.GetCustomAttributes(false).Cast<Attribute>();
                var inherited = ReflectedMember.GetCustomAttributes(true).Cast<Attribute>().Except(notInherited, comparer);

                var attr = notInherited
                    .Select(
                        x => new
                        {
                            Attribute = x,
                            Inherited = false,
                            AttributeUsage = (x.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute), false)).Cast<AttributeUsageAttribute>().Single()
                        })
                    .Union(
                        inherited.Select(
                            x => new
                            {
                                Attribute = x,
                                Inherited = true,
                                AttributeUsage = (x.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute), false)).Cast<AttributeUsageAttribute>().Single()
                            }))
                    .Select(
                        x => new AttributeInfo
                        {
                            Attribute = x.Attribute,
                            AllowMultiple = x.AttributeUsage.AllowMultiple,
                            AttributeTargets = x.AttributeUsage.ValidOn,
                            Inherited = x.Inherited
                        } as IAttributeInfo);
                _attributes = attr.ToArray();

                if (parameterInfo.IsIn)
                {
                    _direction &= ParameterDirection.Input;
                }
                if (parameterInfo.IsOut)
                {
                    _direction &= ParameterDirection.Output;
                }
                if (parameterInfo.IsRetval)
                {
                    _direction &= ParameterDirection.ReturnValue;
                }
            }
        }

        protected internal MethodBaseToken(T info) : base(info, info.MethodHandle, info.DeclaringType, info.Name)
        {
            _accessModifier = GetAccessModifier(info.VerifyArgument(nameof(info)).IsNotNull());
            _declaration = info.GetDeclarationType();
        }

        protected override T GetMember(RuntimeMethodHandle handle, RuntimeTypeHandle typeHandle, bool isGeneric)
        {
            return (T)(isGeneric ? MethodBase.GetMethodFromHandle(handle, typeHandle) : MethodBase.GetMethodFromHandle(handle));
        }
    }
}