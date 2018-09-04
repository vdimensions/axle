#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
namespace Axle.DependencyInjection.Sdk
{
    public abstract partial class AbstractContainer
    {
        private class ParentLookupDependencyResolver : IDependencyResolver
        {
            private readonly DependencyMap _map;
            private readonly IContainer _parentContainer;

            public ParentLookupDependencyResolver(DependencyMap map, IContainer parentContainer)
            {
                _map = map;
                _parentContainer = parentContainer;
            }

            public object Resolve(DependencyInfo dependency)
            {
                try
                {
                    if (!string.IsNullOrEmpty(dependency.DependencyName))
                    {
                        return _map.Resolve(dependency.DependencyName, dependency.Type);
                    }

                    try
                    {
                        return _map.Resolve(dependency.MemberName, dependency.Type);
                    }
                    catch (DependencyNotFoundException)
                    {
                        return _map.Resolve(string.Empty, dependency.Type);
                    }
                }
                catch (DependencyResolutionException)
                {
                    if (_parentContainer == null)
                    {
                        throw;
                    }

                    if (!string.IsNullOrEmpty(dependency.DependencyName))
                    {
                        return _parentContainer.Resolve(dependency.Type, dependency.DependencyName);
                    }

                    try
                    {
                        return _parentContainer.Resolve(dependency.Type, dependency.MemberName);
                    }
                    catch (DependencyNotFoundException)
                    {
                        return _parentContainer.Resolve(dependency.Type, string.Empty);
                    }
                }
            }
        }
    }
}
#endif