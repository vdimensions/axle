using Axle.DependencyInjection;
using Axle.Reflection;
using Axle.Verification;


namespace Axle.Modularity
{
    public sealed class ModuleMethod
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
            switch (_params.Length)
            {
                case 2:
                {
                    _invokable.Invoke(module, exporter, args);
                    break;
                }
                case 1:
                {
                    var arg1 = _params[0].Type.IsArray ? (object) args : (object) exporter;
                    _invokable.Invoke(module, arg1);
                    break;
                }
                default:
                {
                    _invokable.Invoke(module);
                    break;
                }
            }
        }
    }
}