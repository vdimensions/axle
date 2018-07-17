using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || !NETSTANDARD
    [Serializable]
    #endif
	public sealed class WriteOnlyPropertyToken : PropertyToken, IWriteOnlyProperty
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PropertySetAccessor _setAccessor;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly AccessModifier _accessModifier;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DeclarationType _declaration;

        public WriteOnlyPropertyToken(PropertyInfo propertyInfo) : base(propertyInfo)
        {
            #if NETSTANDARD || NET45_OR_NEWER
            var sm = propertyInfo.SetMethod;
            #else
            var sm = propertyInfo.GetSetMethod(true);
            #endif

            if (sm == null)
            {
                throw new ArgumentException("The property is not writeable. ",  nameof(propertyInfo));
            }
            _setAccessor = new PropertySetAccessor(this, new MethodToken(sm));

            var isPublic = sm.IsPublic;
            var isAssembly = sm.IsAssembly && !isPublic;
            var isFamily = sm.IsFamily && !isPublic;
            var isPrivate = sm.IsPrivate && !(isPublic || isFamily || isAssembly);
            _accessModifier = GetAccessModifier(isPublic, isAssembly, isFamily, isPrivate);
            _declaration = sm.GetDeclarationType();
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

        public override AccessModifier AccessModifier => _accessModifier;
        public override DeclarationType Declaration => _declaration;
        public ISetAccessor SetAccessor => _setAccessor;
    }
}