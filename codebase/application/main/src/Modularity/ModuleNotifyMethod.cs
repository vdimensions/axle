using System;
using System.Linq;

using Axle.Reflection;
using Axle.Verification;


namespace Axle.Application.Modularity
{
    public sealed class ModuleNotifyMethod
    {
        private readonly IInvokable _invokable;

        internal ModuleNotifyMethod(IMethod invokable)
        {
            invokable.VerifyArgument(nameof(invokable)).IsNotNull();
            ArgumentType = (_invokable = invokable).GetParameters().Single().Type;
        }

        public void Invoke(object module, object arg)
        {
            _invokable.Invoke(arg);
        }

        public Type ArgumentType { get; }
    }
}