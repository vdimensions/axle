using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Axle.Core.Infrastructure.DependencyInjection.Sdk;
using Axle.Extensions.Object;
using Axle.Reflection;


namespace Axle.Core.Infrastructure.DependencyInjection.Impl
{
    internal sealed class PropertyDependency : IPropertyDependency
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _declaredName;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IProperty _property;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool _isOptional;

        public PropertyDependency(IProperty property, string declaredName, bool isOptional)
        {
            _declaredName = declaredName;
            _property = property;
            _isOptional = isOptional;
        }

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

        public void Set(object target, object value)
        {
            if (_property.SetAccessor == null)
            {
                throw new InvalidOperationException(string.Format("Property {0} has not setter.", _property.Name));
            }
            _property.SetAccessor.SetValue(target, value);
        }

        public IEnumerable<Attribute> GetAttributes() => _property.Attributes.Select(x => x.Attribute);

        public Type RequestedType => _property.MemberType;
        public string DeclaredName => _declaredName;
        public string InferredName => _property.Name;


        public string PropertyName => _property.Name;
        public bool IsOptional => _isOptional;
    }
}