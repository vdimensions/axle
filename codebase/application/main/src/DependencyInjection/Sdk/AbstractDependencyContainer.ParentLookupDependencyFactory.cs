#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK || UNITY_2018_1_OR_NEWER
namespace Axle.DependencyInjection.Sdk
{
    public abstract partial class AbstractDependencyContainer
    {
        private sealed class ParentLookupDependencyResolver : IDependencyResolver
        {
            private readonly DependencyMap _map;
            private readonly IDependencyContext _parentDependencyContainer;

            public ParentLookupDependencyResolver(DependencyMap map, IDependencyContext parentDependencyContainer)
            {
                _map = map;
                _parentDependencyContainer = parentDependencyContainer;
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
                    if (_parentDependencyContainer == null)
                    {
                        throw;
                    }

                    DependencyResolutionException lastEx;
                    var pc = _parentDependencyContainer;
                    do
                    {
                        if (!string.IsNullOrEmpty(dependency.DependencyName))
                        {
                            return pc.Resolve(dependency.Type, dependency.DependencyName);
                        }

                        try
                        {
                            return pc.Resolve(dependency.Type, dependency.MemberName);
                        }
                        catch (DependencyNotFoundException)
                        {
                            try
                            {
                                return pc.Resolve(dependency.Type, string.Empty);
                            }
                            catch (DependencyNotFoundException e)
                            {
                                lastEx = e;
                                pc = pc.Parent;
                            }
                        }
                    }
                    while (pc != null);

                    throw lastEx;
                }
            }
        }
    }
}
#endif