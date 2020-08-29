using System.Threading.Tasks;
using Axle.DependencyInjection;
using Axle.Reflection;
using Axle.Verification;


namespace Axle.Modularity
{
    internal sealed class ModuleMethod
    {
        private readonly IInvokable _invokable;
        private readonly IParameter[] _params;

        internal ModuleMethod(IInvokable invokable)
        {
            _invokable = invokable.VerifyArgument(nameof(invokable)).IsNotNull().Value;
            _params = invokable.GetParameters();
        }

        public void Invoke(object module, IDependencyExporter exporter, string[] args)
        {
            Task task;
            switch (_params.Length)
            {
                case 2:
                {
                    task = _invokable.Invoke(module, exporter, args) as Task;
                    break;
                }
                case 1:
                {
                    var arg1 = _params[0].Type.IsArray ? (object) args : (object) exporter;
                    task = _invokable.Invoke(module, arg1) as Task;
                    break;
                }
                default:
                {
                    task = _invokable.Invoke(module) as Task;
                    break;
                }
            }
            // in case the method was async (returning a task), we should wait for it to complete
            if (task != null && !task.IsCompleted)
            {
                task.Wait();
            }
        }
    }
}