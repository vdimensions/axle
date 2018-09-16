#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
namespace Axle.DependencyInjection.Sdk
{
    public abstract partial class AbstractContainer
    {
        private sealed class ParentLookupDependencyResolver : IDependencyResolver
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

                    DependencyResolutionException lastEx;
                    var pc = _parentContainer;
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