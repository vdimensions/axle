namespace Axle.Reflection
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class PropertyGetAccessor : PropertyAccessor, IGetAccessor
    {
        public PropertyGetAccessor(PropertyToken property, MethodToken operationMethod) : base(property, operationMethod) { }

        public object GetValue(object target) { return OperationMethod.Invoke(target); }

        public override AccessorType AccessorType => AccessorType.Get;
    }
}