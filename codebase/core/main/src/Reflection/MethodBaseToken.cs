using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


namespace Axle.Reflection
{
    /// <summary>
    /// An abstract class representing the common reflected data from a class method or constructor.
    /// </summary>
    /// <typeparam name="T">
    /// A suitable implementation of the <see cref="MethodBase"/> class representing the underlying reflected member for the current <see cref="MethodBaseToken{T}"/> instance.
    /// </typeparam>
    public abstract partial class MethodBaseToken<T> : IEquatable<MethodBaseToken<T>>
    {
        internal sealed partial class Parameter : IParameter
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly ParameterInfo _parameterInfo;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly IEnumerable<IAttributeInfo> _attributes;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly ParameterDirection _direction;

            public string Name => _parameterInfo.Name;
            public Type Type => _parameterInfo.ParameterType;
            public bool IsOptional => _parameterInfo.IsOptional;
            public object DefaultValue => _parameterInfo.DefaultValue;
            public ParameterInfo ReflectedMember => _parameterInfo;
            public IEnumerable<IAttributeInfo> Attributes => _attributes;
            public ParameterDirection Direction => _direction;
        }

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

        public bool Equals(MethodBaseToken<T> other) { return base.Equals(other); }

        public override AccessModifier AccessModifier => _accessModifier;
        public override DeclarationType Declaration => _declaration;
    }
}