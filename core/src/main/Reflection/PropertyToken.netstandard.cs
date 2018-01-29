using System.Reflection;


namespace Axle.Reflection
{
    partial class PropertyToken
    {
        public PropertyToken(PropertyInfo propertyInfo) : base(propertyInfo, propertyInfo.DeclaringType, propertyInfo.Name)
        {
            property = propertyInfo;
            var gm = propertyInfo.GetMethod;
            var sm = propertyInfo.SetMethod;
            getAccessor = gm != null ? new PropertyGetAccessor(this, new MethodToken(gm)) : null;
            setAccessor = sm != null ? new PropertySetAccessor(this, new MethodToken(sm)) : null;
            memberType = propertyInfo.PropertyType;

            declaration = ReflectionExtensions.GetDeclarationTypeUnchecked(gm, sm);

            var isPublic = (gm == null || gm.IsPublic) && (sm == null || sm.IsPublic);
            var isAssembly = (gm == null || gm.IsAssembly) && (sm == null || sm.IsAssembly) && !isPublic;
            var isFamily = (gm == null || gm.IsFamily) && (sm == null || sm.IsFamily) && !isPublic;
            var isPrivate = (gm == null || gm.IsPrivate) && (sm == null || sm.IsPrivate) &&
                            !(isPublic || isFamily || isAssembly);
            accessModifier = GetAccessModifier(isPublic, isAssembly, isFamily, isPrivate);
        }

    }
}