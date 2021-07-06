using System;
using System.Linq;
using System.Threading.Tasks;
using Axle.Reflection;
using Axle.Verification;

namespace Axle.Modularity
{
    internal sealed class ModuleCallback
    {
        private readonly IInvokable _invokable;

        internal ModuleCallback(IMethod invokable, int priority, bool allowParallelInvoke)
        {
            invokable.VerifyArgument(nameof(invokable)).IsNotNull();
            ArgumentType = (_invokable = invokable).GetParameters().Single().Type;
            Priority = priority;
            AllowParallelInvoke = allowParallelInvoke;
        }

        public void Invoke(object module, object arg)
        {
            if (_invokable.Invoke(module, arg) is Task task && !task.IsCompleted)
            {
                // in case the method was async (returning a task), we should wait for it to complete
                task.Wait();
            }
        }

        public int Priority { get; }
        public Type ArgumentType { get; }
        public bool AllowParallelInvoke { get; }
    }
}