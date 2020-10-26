using System;
using System.Linq;
using System.Reflection;
using Axle.Reflection;

namespace Axle.Application.Services
{
    internal sealed class ServiceInfo
    {
        public static bool TryGetServiceInfo(object instance, out ServiceInfo result)
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
                        Attribute = new TypeIntrospector(t).GetAttributes<ServiceAttribute>().Select(x => x.Attribute).Cast<ServiceAttribute>().SingleOrDefault()
                    })
                .SingleOrDefault(x => x.Attribute != null);
            result = serviceType == null ? null : new ServiceInfo(instance, serviceType.Type, serviceType.Attribute.Name);
            return result != null;
        }
        
        private readonly object _instance;
        private readonly Type _serviceType;
        private readonly string _name;

        private ServiceInfo(object instance, Type serviceType, string name)
        {
            _instance = instance;
            _serviceType = serviceType;
            _name = name;
        }

        public object Instance => _instance;
        public Type ServiceType => _serviceType;
        public string Name => _name;
    }
}