using Axle.DependencyInjection;

namespace Axle.Application.Unity
{
    public abstract class AbstractUnityApplicationHost : AbstractApplicationHost
    {
        protected AbstractUnityApplicationHost(
                IDependencyContainerFactory dependencyContainerFactory, 
                string appConfigName, 
                string appHostConfigName,
                string environmentName, 
                params string[] profiles) 
            : base(dependencyContainerFactory, null, appConfigName, appHostConfigName, environmentName, profiles) { }
        protected AbstractUnityApplicationHost(
                IDependencyContainerFactory dependencyContainerFactory, 
                string environmentName, 
                params string[] profiles) 
            : this(dependencyContainerFactory, null, null, environmentName, profiles) { }
    }
}