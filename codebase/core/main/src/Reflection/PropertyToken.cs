using System;
using System.Diagnostics;
using System.Reflection;

using Axle.Verification;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class PropertyToken : MemberTokenBase<PropertyInfo>, IProperty, IEquatable<PropertyToken>
    {
        public static PropertyToken Create(PropertyInfo property)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(property, nameof(property)));

            if (property.CanRead)
            {
                return property.CanWrite 
                    ? new ReadWritePropertyToken(property) 
                    : new ReadOnlyPropertyToken(property) as PropertyToken;
            }

            if (property.CanWrite)
            {
                return new WriteOnlyPropertyToken(property);
            }

            throw new InvalidOperationException(
                "Cannot create a property token object from a property that cannot be read and written.");
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertyInfo _property;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _memberType;

        protected PropertyToken(PropertyInfo propertyInfo) 
            : base(propertyInfo, propertyInfo.DeclaringType, propertyInfo.Name)
        {
            _property = propertyInfo;
            _memberType = propertyInfo.PropertyType;
        }

        public bool Equals(PropertyToken other)
        {
            return base.Equals(other);/*
                && DeclaringType == other.DeclaringType
                && MemberType == other.MemberType
                && ((Name == null && other.Name == null) || 
                    (Name != null && Name.Equals(other.Name, StringComparison.Ordinal)))
                && object.Equals(_getAccessor, other._getAccessor)
                && object.Equals(_setAccessor, other._setAccessor);
                */
        }

        public override PropertyInfo ReflectedMember => _property;
        public override Type MemberType => _memberType;
    }
}