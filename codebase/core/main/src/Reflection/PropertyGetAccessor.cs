#if NETSTANDARD || NET35_OR_NEWER
namespace Axle.Reflection
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class PropertyGetAccessor : PropertyAccessor, IGetAccessor
    {
        public PropertyGetAccessor(PropertyToken property, MethodToken operationMethod) : base(property, operationMethod) { }

        public object GetValue(object target) => OperationMethod.Invoke(target);

        public override AccessorType AccessorType => AccessorType.Get;
    }
}
#endif