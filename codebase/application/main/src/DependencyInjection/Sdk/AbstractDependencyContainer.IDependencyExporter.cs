#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using Axle.Verification;

namespace Axle.DependencyInjection.Sdk
{
    partial class AbstractDependencyContainer : IDependencyExporter
    {
        IDependencyExporter IDependencyExporter.Export(object instance, string name)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(instance, nameof(instance)));
            return RegisterInstance(instance, name);
        }
    }
}
#endif