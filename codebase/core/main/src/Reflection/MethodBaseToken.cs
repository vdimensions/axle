using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Axle.Reflection.Extensions;
using Axle.Verification;


namespace Axle.Reflection
{
    /// <summary>
    /// An abstract class representing the common reflected data from a class method or constructor.
    /// </summary>
    /// <typeparam name="T">
    /// A suitable implementation of the <see cref="MethodBase"/> class representing the underlying reflected member for the current <see cref="MethodBaseToken{T}"/> instance.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
	public abstract class MethodBaseToken<T> : MemberTokenBase<T, RuntimeMethodHandle>, IEquatable<MethodBaseToken<T>>
    #else
    public abstract class MethodBaseToken<T> : MemberTokenBase<T>, IEquatable<MethodBaseToken<T>> 
    #endif
        where T: MethodBase
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [Serializable]
        #endif
        internal sealed class Parameter : IParameter
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly ParameterInfo _parameterInfo;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly ParameterDirection _direction;

            public Parameter(ParameterInfo parameterInfo)
            {
                _parameterInfo = parameterInfo.VerifyArgument(nameof(parameterInfo)).IsNotNull();

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

            public IAttributeInfo[] GetAttributes() => ReflectedMember.GetEffectiveAttributes();
            public IAttributeInfo[] GetAttributes(Type attributeType) => ReflectedMember.GetEffectiveAttributes(attributeType);
            public IAttributeInfo[] GetAttributes(Type attributeType, bool inherit)
            {
                var reflectedMember = ReflectedMember;
                var attrs = reflectedMember.GetCustomAttributes(attributeType, inherit).Cast<Attribute>();
                return inherit
                    ? CustomAttributeProviderExtensions.FilterAttributes(new Attribute[0], attrs)
                    : CustomAttributeProviderExtensions.FilterAttributes(attrs, new Attribute[0]);
            }

            public bool IsDefined(Type attributeType, bool inherit)
            {
                #if NETSTANDARD
                return ReflectedMember.IsDefined(attributeType);
                #else
                return ReflectedMember.IsDefined(attributeType, inherit);
                #endif
            }

            public string Name => _parameterInfo.Name;
            public Type Type => _parameterInfo.ParameterType;
            public bool IsOptional => _parameterInfo.IsOptional;
            public object DefaultValue => _parameterInfo.DefaultValue;
            public ParameterInfo ReflectedMember => _parameterInfo;
            [Obsolete("Use GetAttributes instead.")]
            public IEnumerable<IAttributeInfo> Attributes => GetAttributes();
            public ParameterDirection Direction => _direction;
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected internal MethodBaseToken(T info) : base(info, info.MethodHandle, info.DeclaringType, info.Name)
        #else
        protected internal MethodBaseToken(T info) : base(info, info.DeclaringType, info.Name)
        #endif
        {
            _accessModifier = GetAccessModifier(info.VerifyArgument(nameof(info)).IsNotNull());
            _declaration = info.GetDeclarationType();
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected override T GetMember(RuntimeMethodHandle handle, RuntimeTypeHandle typeHandle, bool isGeneric)
        {
            return (T)(isGeneric ? MethodBase.GetMethodFromHandle(handle, typeHandle) : MethodBase.GetMethodFromHandle(handle));
        }
        #endif

        public static AccessModifier GetAccessModifier(MethodBase methodBase)
        {
            return GetAccessModifier(methodBase.IsPublic, methodBase.IsAssembly, methodBase.IsFamily, methodBase.IsPrivate);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier _accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType _declaration;

        public IParameter[] GetParameters() { return GetParameters(ReflectedMember.GetParameters()); }
        protected virtual IParameter[] GetParameters(IEnumerable<ParameterInfo> reflectedParameters)
        {
            return reflectedParameters.Select(x => new Parameter(x)).Cast<IParameter>().ToArray();
        }

        public bool Equals(MethodBaseToken<T> other) => base.Equals(other);

        public override AccessModifier AccessModifier => _accessModifier;
        public override DeclarationType Declaration => _declaration;
    }
}