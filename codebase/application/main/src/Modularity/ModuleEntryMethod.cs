using Axle.Reflection;


namespace Axle.Application.Modularity
{
    public sealed class ModuleEntryMethod
    {
        private readonly IInvokable _invokable;

        internal ModuleEntryMethod(IInvokable invokable)
        {
            _invokable = invokable;
        }

        public void Invoke(object module, string[] args)
        {
            if (_invokable.GetParameters().Length == 0)
            {
                _invokable.Invoke(module);
            }
            else
            {
                _invokable.Invoke(module, args);
            }
        }
    }
}