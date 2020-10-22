using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    /// <summary>
    /// Represents a reflected write-only property member. 
    /// </summary>
    /// <seealso cref="IWriteOnlyProperty"/>
    /// <seealso cref="PropertyToken"/>
    #if NETFRAMEWORK
    [Serializable]
    #endif
    internal sealed class WriteOnlyPropertyToken : PropertyToken, IWriteOnlyProperty
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertySetAccessor _setAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier _accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType _declaration;

        /// <summary>
        /// Creates a new instance of the <see cref="WriteOnlyPropertyToken"/> class using the specified by the
        /// <paramref name="propertyInfo"/> parameter <see cref="PropertyInfo">value</see>.
        /// </summary>
        /// <param name="propertyInfo">
        /// A <see cref="PropertyInfo"/> representing the reflected write-only property.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="propertyInfo"/> does not represent a writeable property.
        /// </exception>
        public WriteOnlyPropertyToken(PropertyInfo propertyInfo) : base(propertyInfo)
        {
            #if NETSTANDARD
            var sm = propertyInfo.SetMethod;
            #else
            var sm = propertyInfo.GetSetMethod(true);
            #endif

            if (sm == null)
            {
                throw new ArgumentException(
                    string.Format("The property '{0}' is not writable. ", propertyInfo.Name), 
                    nameof(propertyInfo));
            }
            _setAccessor = new PropertySetAccessor(this, new MethodToken(sm));

            var isPublic = sm.IsPublic;
            var isAssembly = sm.IsAssembly && !isPublic;
            var isFamily = sm.IsFamily && !isPublic;
            var isPrivate = sm.IsPrivate && !(isPublic || isFamily || isAssembly);
            _accessModifier = AccessModifierExtensions.GetAccessModifier(isPublic, isAssembly, isFamily, isPrivate);
            _declaration = ReflectionExtensions.GetDeclarationType(sm);
        }

        IAccessor IAccessible.FindAccessor(AccessorType accessorType)
        {
            switch (accessorType)
            {
                case AccessorType.Set: return _setAccessor;
                default: return null;
            }
        }

        IEnumerable<IAccessor> IAccessible.Accessors => new IAccessor[] { SetAccessor };

        /// <inheritdoc />
        public override AccessModifier AccessModifier => _accessModifier;
        
        /// <inheritdoc />
        public override DeclarationType Declaration => _declaration;

        /// <inheritdoc />
        public ISetAccessor SetAccessor => _setAccessor;
    }
}