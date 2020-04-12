using Axle.DependencyInjection;
using Axle.Verification;

namespace Axle
{
    internal sealed class ApplicationContainerFactory : IDependencyContainerFactory
    {
        private readonly IDependencyContainerFactory _factory;
        private readonly IDependencyContext _rootContext;

        public ApplicationContainerFactory(IDependencyContainerFactory factory, IDependencyContext rootContext)
        {
            _factory = factory;
            _rootContext = rootContext;
        }

        public IDependencyContainer CreateContainer() => _factory.CreateContainer(_rootContext);
        public IDependencyContainer CreateContainer(IDependencyContext parent)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(parent, nameof(parent)));
            return _factory.CreateContainer(parent);
        }
    }
}