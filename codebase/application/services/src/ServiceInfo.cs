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
                .SingleOrDefault(t => new TypeIntrospector(t).GetAttributes<ServiceAttribute>().Length > 0);
            result = serviceType == null ? null : new ServiceInfo(instance, serviceType);
            return result != null;
        }
        
        private readonly object _instance;
        private readonly Type _serviceType;

        private ServiceInfo(object instance, Type serviceType)
        {
            _instance = instance;
            _serviceType = serviceType;
        }

        public object Instance => _instance;
        public Type ServiceType => _serviceType;
    }
}