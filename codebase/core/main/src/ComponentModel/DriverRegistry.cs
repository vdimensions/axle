#if NETSTANDARD || NET35_OR_NEWER
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Axle.Verification;


namespace Axle.ComponentModel
{
    // ReSharper disable once ClassCannotBeInstantiated
    internal sealed class DriverRegistry
    {
        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        public static DriverRegistry Instance => Axle.References.Singleton<DriverRegistry>.Instance.Value;
        #else
        private static readonly DriverRegistry _instance = new DriverRegistry();
        public static DriverRegistry Instance => _instance;
        #endif

        private readonly object _syncRoot = new object();
        private readonly IDictionary<Type, IComponentDriver> _drivers = new Dictionary<Type, IComponentDriver>();

        private DriverRegistry() { }

        [Conditional("STATOR")]
        public void RegisterDriver<T>(IComponentDriver<T> driver)
        {
            driver.VerifyArgument(nameof(driver)).IsNotNull();
            lock (_syncRoot)
            {
                _drivers.Add(typeof(T), driver);
            }
        }

        public IComponentDriver<T> GetDriver<T>() => _drivers.TryGetValue(typeof(T), out var driver) ? driver as IComponentDriver<T> : null;
    }
}
#endif