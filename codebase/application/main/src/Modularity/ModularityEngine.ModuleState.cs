using System;


namespace Axle.Modularity
{
    partial class ModularityEngine
    {
        [Flags]
        internal enum ModuleState : sbyte
        {
            Hollow = 0,
            Instantiated = 1,
            Initialized = 2,
            Ran = 4,
            Terminated = -1,
        }
    }
}