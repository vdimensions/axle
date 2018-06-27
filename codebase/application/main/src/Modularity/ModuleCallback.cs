using System;
using System.Linq;

using Axle.Reflection;
using Axle.Verification;


namespace Axle.Application.Modularity
{
    public sealed class ModuleCallback
    {
        private readonly IInvokable _invokable;

        internal ModuleCallback(IMethod invokable, int priority)
        {
            invokable.VerifyArgument(nameof(invokable)).IsNotNull();
            ArgumentType = (_invokable = invokable).GetParameters().Single().Type;
            Priority = priority;
        }

        public void Invoke(object module, object arg)
        {
            _invokable.Invoke(arg);
        }

        public int Priority { get; }
        public Type ArgumentType { get; }
    }
}