using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Axle.Core.Infrastructure.DependencyInjection.Sdk;
using Axle.Extensions.Object;
using Axle.Reflection;


namespace Axle.Core.Infrastructure.DependencyInjection.Impl
{
    internal sealed class ParameterDependency : IDependency
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _declaredName;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IParameter _parameter;

        public ParameterDependency(IParameter parameter, string declaredName)
        {
            _declaredName = declaredName;
            _parameter = parameter;
        }

        public IEnumerable<Attribute> GetAttributes() => _parameter.Attributes.Select(x => x.Attribute);

        public bool Equals(IDependency other)
        {
            var cmp = StringComparer.Ordinal;
            return other != null
                && RequestedType == other.RequestedType
                && cmp.Equals(DeclaredName, other.DeclaredName)
                && cmp.Equals(InferredName, other.InferredName);
        }
        public override bool Equals(object obj) => obj is IDependency d && Equals(d);

        public override int GetHashCode() => this.CalculateHashCode(RequestedType, DeclaredName, InferredName);

        public Type RequestedType => _parameter.Type;
        public string DeclaredName => _declaredName;
        public string InferredName => _parameter.Name;
        public bool IsOptional => _parameter.IsOptional;
    }
}