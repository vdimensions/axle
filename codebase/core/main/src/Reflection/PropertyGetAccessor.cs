namespace Axle.Reflection
{
    #if NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class PropertyGetAccessor : PropertyAccessor, IGetAccessor
    {
        public PropertyGetAccessor(PropertyToken property, MethodToken operationMethod) : base(property, operationMethod) { }

        public object GetValue(object target) => OperationMethod.Invoke(target);

        public override AccessorType AccessorType => AccessorType.Get;
    }
}