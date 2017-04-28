using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


namespace Axle.Reflection
{
    public abstract partial class MethodBaseToken<T> : IEquatable<MethodBaseToken<T>>
    {
        internal sealed partial class Parameter : IParameter
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly ParameterInfo parameterInfo;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly IEnumerable<IAttributeInfo> attributes;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly ParameterDirection direction;

            public string Name { get { return parameterInfo.Name; } }
            public Type Type { get { return parameterInfo.ParameterType; } }
            public bool IsOptional { get { return parameterInfo.IsOptional; } }
            public object DefaultValue { get { return parameterInfo.DefaultValue; } }
            public ParameterInfo ReflectedMember { get { return parameterInfo; } }
            public IEnumerable<IAttributeInfo> Attributes { get { return attributes; } }
            public ParameterDirection Direction { get { return direction; } }
        }

        public static AccessModifier GetAccessModifier(MethodBase methodBase)
        {
            return GetAccessModifier(methodBase.IsPublic, methodBase.IsAssembly, methodBase.IsFamily, methodBase.IsPrivate);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType declaration;

        public IParameter[] GetParameters() { return GetParameters(ReflectedMember.GetParameters()); }
        protected virtual IParameter[] GetParameters(IEnumerable<ParameterInfo> reflectedParameters)
        {
            return reflectedParameters.Select(x => new Parameter(x)).Cast<IParameter>().ToArray();
        }

        public bool Equals(MethodBaseToken<T> other) { return base.Equals(other); }

        public override AccessModifier AccessModifier { get { return accessModifier; } }
        public override DeclarationType Declaration { get { return declaration; } }
    }
}