using Axle.Reflection;


namespace Axle.Application.Modularity
{
    public sealed class ModuleMethod
    {
        private readonly IInvokable _invokable;

        internal ModuleMethod(IInvokable invokable)
        {
            _invokable = invokable;
        }

        public void Invoke(object module, IModuleExporter exporter)
        {
            if (_invokable.GetParameters().Length == 0)
            {
                _invokable.Invoke(module);
            }
            else
            {
                _invokable.Invoke(module, exporter);
            }
        }
    }
}