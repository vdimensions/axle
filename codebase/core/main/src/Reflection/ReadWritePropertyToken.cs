using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class ReadWritePropertyToken : PropertyToken, IReadWriteProperty
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertyGetAccessor _getAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertySetAccessor _setAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier _accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType _declaration;

        public ReadWritePropertyToken(PropertyInfo propertyInfo) : base(propertyInfo)
        {
            #if NETSTANDARD || NET45_OR_NEWER
            var gm = propertyInfo.GetMethod;
            var sm = propertyInfo.SetMethod;
            #else
            var gm = propertyInfo.GetGetMethod(true);
            var sm = propertyInfo.GetSetMethod(true);
            #endif

            if (gm == null)
            {
                throw new ArgumentException("The property is not readable. ",  nameof(propertyInfo));
            }
            if (sm == null)
            {
                throw new ArgumentException("The property is not writable. ", nameof(propertyInfo));
            }

            _getAccessor = new PropertyGetAccessor(this, new MethodToken(gm));
            _setAccessor = new PropertySetAccessor(this, new MethodToken(sm));

            var isPublic = gm.IsPublic && sm.IsPublic;
            var isAssembly = gm.IsAssembly && sm.IsAssembly && !isPublic;
            var isFamily = gm.IsFamily && sm.IsFamily && !isPublic;
            var isPrivate = gm.IsPrivate && sm.IsPrivate && !(isPublic || isFamily || isAssembly);
            _accessModifier = AccessModifierExtensions.GetAccessModifier(isPublic, isAssembly, isFamily, isPrivate);
            _declaration = ReflectionExtensions.GetDeclarationTypeUnchecked(gm, sm);
        }

        IAccessor IAccessible.FindAccessor(AccessorType accessorType)
        {
            switch (accessorType)
            {
                case AccessorType.Get: return _getAccessor;
                case AccessorType.Set: return _setAccessor;
                default: return null;
            }
        }

        IEnumerable<IAccessor> IAccessible.Accessors => new IAccessor[] { GetAccessor, SetAccessor };

        public override AccessModifier AccessModifier => _accessModifier;
        public override DeclarationType Declaration => _declaration;
        public IGetAccessor GetAccessor => _getAccessor;
        public ISetAccessor SetAccessor => _setAccessor;
    }
}