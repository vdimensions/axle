using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;


namespace Axle.References
{
    partial class Singleton<T>
    {
        internal static T CreateInstance()
        {
            var type = typeof(T);
            var constructor = Singleton.GetSingletonConstructor(type);
            if (constructor == null)
            {
                throw new InvalidOperationException("The class '" + type.FullName + "' has one or more public constructors and cannot be used with the singleton pattern!");
            }
            var instance = (T) constructor.Invoke(new object[0]);
            Thread.MemoryBarrier();
            return instance;
        }
    }

    partial class Singleton
    {
        internal static ConstructorInfo GetSingletonConstructor(Type type)
        {
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var hasPublicConstructors = constructors.Any(x => x.IsPublic);
            ConstructorInfo constructor = null;
            if (!hasPublicConstructors)
            {
                constructor = constructors.Where(x => x.GetParameters().Length == 0).SingleOrDefault();
            }
            return constructor;
        }

        public static object GetSingletonInstance(Type type)
        {
            var constructor = GetSingletonConstructor(type);
            if (constructor == null)
            {
                // type cannot be instantiated via singleton
                return null;
            }
            var instanceProperty = typeof(Singleton<>)
                .MakeGenericType(type)
                .GetProperty("Instance", BindingFlags.Public|BindingFlags.Static);
            var res = instanceProperty.GetGetMethod().Invoke(null, null);
            return res != null ? ((IReference) res).Value : null;
        }
    }
}