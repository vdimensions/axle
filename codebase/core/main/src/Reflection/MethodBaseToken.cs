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
    /// A suitable implementation of the <see cref="MethodBase"/> class representing the underlying reflected member 
    /// for the current <see cref="MethodBaseToken{T}"/> instance.
    /// </typeparam>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    internal abstract class MethodBaseToken<T> : MemberTokenBase<T, RuntimeMethodHandle>, IEquatable<MethodBaseToken<T>>
    #else
    internal abstract class MethodBaseToken<T> : MemberTokenBase<T>, IEquatable<MethodBaseToken<T>>
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
                _parameterInfo = Verifier.IsNotNull(Verifier.VerifyArgument(parameterInfo, nameof(parameterInfo)));

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

            public IAttributeInfo[] GetAttributes() => 
                CustomAttributeProviderExtensions.GetEffectiveAttributes(ReflectedMember);
            public IAttributeInfo[] GetAttributes(Type attributeType) => 
                CustomAttributeProviderExtensions.GetEffectiveAttributes(ReflectedMember, attributeType);
            public IAttributeInfo[] GetAttributes(Type attributeType, bool inherit)
            {
                var reflectedMember = ReflectedMember;
                #if NETSTANDARD || NET45_OR_NEWER
                var attrs = Enumerable.Cast<Attribute>(CustomAttributeExtensions.GetCustomAttributes(
                    reflectedMember, 
                    attributeType, 
                    inherit));
                #else
                var attrs = Enumerable.Cast<Attribute>(reflectedMember.GetCustomAttributes(attributeType, inherit));
                #endif
                return inherit
                    ? CustomAttributeProviderExtensions.FilterAttributes(new Attribute[0], attrs)
                    : CustomAttributeProviderExtensions.FilterAttributes(attrs, new Attribute[0]);
            }

            public bool IsDefined(Type attributeType, bool inherit)
            {
                #if NETSTANDARD
                return CustomAttributeExtensions.IsDefined(ReflectedMember, attributeType);
                #else
                return ReflectedMember.IsDefined(attributeType, inherit);
                #endif
            }

            public string Name => _parameterInfo.Name;
            public Type Type => _parameterInfo.ParameterType;
            public bool IsOptional => _parameterInfo.IsOptional;
            public object DefaultValue => _parameterInfo.DefaultValue;
            public ParameterInfo ReflectedMember => _parameterInfo;
            public ParameterDirection Direction => _direction;
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected internal MethodBaseToken(T info) : base(info, info.MethodHandle, info.DeclaringType, info.Name)
        #else
        protected internal MethodBaseToken(T info) : base(info, info.DeclaringType, info.Name)
        #endif
        {
            _accessModifier = GetAccessModifier(Verifier.IsNotNull(Verifier.VerifyArgument(info, nameof(info))));
            _declaration = ReflectionExtensions.GetDeclarationType(info);
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        protected override T GetMember(RuntimeMethodHandle handle, RuntimeTypeHandle typeHandle, bool isGeneric) =>
            (T) (isGeneric 
                ? MethodBase.GetMethodFromHandle(handle, typeHandle) 
                : MethodBase.GetMethodFromHandle(handle));
        #endif

        public static AccessModifier GetAccessModifier(MethodBase methodBase) =>
            AccessModifierExtensions.GetAccessModifier(
                methodBase.IsPublic, 
                methodBase.IsAssembly, 
                methodBase.IsFamily, 
                methodBase.IsPrivate);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier _accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType _declaration;

        public IParameter[] GetParameters() => GetParameters(ReflectedMember.GetParameters());
        protected virtual IParameter[] GetParameters(IEnumerable<ParameterInfo> reflectedParameters)
        {
            return Enumerable.ToArray(
                Enumerable.Cast<IParameter>(
                    Enumerable.Select(reflectedParameters, x => new Parameter(x))));
        }

        public bool Equals(MethodBaseToken<T> other) => base.Equals(other);

        public override AccessModifier AccessModifier => _accessModifier;
        public override DeclarationType Declaration => _declaration;
    }
}