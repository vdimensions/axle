using Axle.Reflection;
using Axle.Verification;

namespace Axle.Application.Modularity
{
    public sealed class ModuleEntryMethod
    {
        private readonly IInvokable _invokable;
        private readonly bool _hasParams;

        internal ModuleEntryMethod(IInvokable invokable)
        {
            _invokable = invokable.VerifyArgument(nameof(invokable)).IsNotNull().Value;
            _hasParams = invokable.GetParameters().Length > 0;
        }

        public void Invoke(object module, string[] args)
        {
            if (_hasParams)
            {
                _invokable.Invoke(module, args as object);
            }
            else
            {
                _invokable.Invoke(module);
            }
        }
    }
}