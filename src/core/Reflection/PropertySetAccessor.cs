using System;


namespace Axle.Reflection
{
#if !netstandard
    [Serializable]
#endif

    internal sealed class PropertySetAccessor : PropertyAccessor, ISetAccessor
    {
        public PropertySetAccessor(PropertyToken property, MethodToken operationMethod) : base(property, operationMethod) { }

        /// <summary>
        /// Sets the value of the property to the specified by the parameter <paramref name="value"/> parameter object.
        /// </summary>
        /// <param name="target">
        /// The target object owning the property. Use <c>null</c> if the property is static.
        /// </param>
        /// <param name="value">
        /// The value to be set to the target property.
        /// </param>
        /// <seealso cref="IProperty"/>
        /// <seealso cref="System.Reflection.PropertyInfo"/>
        public void SetValue(object target, object value) { OperationMethod.Invoke(target, value); }

        public override AccessorType AccessorType { get { return AccessorType.Set; } }
    }
}