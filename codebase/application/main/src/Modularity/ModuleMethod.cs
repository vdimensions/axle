using Axle.Reflection;
using Axle.Verification;


namespace Axle.Modularity
{
    public sealed class ModuleMethod
    {
        private readonly IInvokable _invokable;
        private readonly bool _hasParams;

        internal ModuleMethod(IInvokable invokable)
        {
            _invokable = invokable.VerifyArgument(nameof(invokable)).IsNotNull().Value;
            _hasParams = invokable.GetParameters().Length > 0;
        }

        public void Invoke(object module, ModuleExporter exporter)
        {
            if (_hasParams)
            {
                _invokable.Invoke(module, exporter);
            }
            else
            {
                _invokable.Invoke(module);
            }
        }
    }
}