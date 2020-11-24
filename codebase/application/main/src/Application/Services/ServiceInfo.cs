using System;
using System.Linq;
using Axle.Reflection;
#if NETSTANDARD
using System.Reflection;
#endif

namespace Axle.Application.Services
{
    internal sealed class ServiceInfo<TService>
    {
        public static bool TryGetServiceInfo(object instance, out ServiceInfo<TService> result)
        {
            var type = instance.GetType();
            var serviceType = new[]{type}
                #if NETSTANDARD
                .Union(type.GetTypeInfo().GetInterfaces())
                #else
                .Union(type.GetInterfaces())
                #endif
                .Select(
                    t => new
                    {
                        Type = t, 
                        Attribute = new TypeIntrospector(t).GetAttributes<AbstractServiceAttribute>().Select(x => x.Attribute).Cast<AbstractServiceAttribute>().SingleOrDefault()
                    })
                .SingleOrDefault(x => x.Attribute != null);
            result = serviceType == null || !(instance is TService service)
                ? null 
                : new ServiceInfo<TService>(service, serviceType.Type, serviceType.Attribute.Name, serviceType.Attribute.IsPublic);
            return result != null;
        }
        
        private readonly TService _instance;
        private readonly Type _serviceType;
        private readonly string _name;
        private readonly bool _isPublic;

        private ServiceInfo(TService instance, Type serviceType, string name, bool isPublic)
        {
            _instance = instance;
            _serviceType = serviceType;
            _name = name;
            _isPublic = isPublic;
        }

        public TService Instance => _instance;
        public Type ServiceType => _serviceType;
        public string Name => _name;
        public bool IsPublic  => _isPublic;
    }
}