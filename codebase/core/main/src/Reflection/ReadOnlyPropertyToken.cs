using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    internal sealed class ReadOnlyPropertyToken : PropertyToken, IReadOnlyProperty
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertyGetAccessor _getAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier _accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType _declaration;

        public ReadOnlyPropertyToken(PropertyInfo propertyInfo) : base(propertyInfo)
        {
            #if NETSTANDARD || NET45_OR_NEWER
            var gm = propertyInfo.GetMethod;
            #else
            var gm = propertyInfo.GetGetMethod(true);
            #endif

            if (gm == null)
            {
                throw new ArgumentException("The property is not readable. ",  nameof(propertyInfo));
            }
            _getAccessor = new PropertyGetAccessor(this, new MethodToken(gm));

            var isPublic = gm.IsPublic;
            var isAssembly = gm.IsAssembly && !isPublic;
            var isFamily = gm.IsFamily && !isPublic;
            var isPrivate = gm.IsPrivate && !(isPublic || isFamily || isAssembly);
            _accessModifier = AccessModifierExtensions.GetAccessModifier(isPublic, isAssembly, isFamily, isPrivate);
            _declaration = ReflectionExtensions.GetDeclarationType(gm);
        }

        IAccessor IAccessible.FindAccessor(AccessorType accessorType)
        {
            switch (accessorType)
            {
                case AccessorType.Get:
                    return _getAccessor;
                default:
                    return null;
            }
        }

        IEnumerable<IAccessor> IAccessible.Accessors => new IAccessor[] { GetAccessor };

        public override AccessModifier AccessModifier => _accessModifier;
        public override DeclarationType Declaration => _declaration;
        public IGetAccessor GetAccessor => _getAccessor;
    }
}