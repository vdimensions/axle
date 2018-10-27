using System;


namespace Axle.Modularity
{
    partial class ModularContext
    {
        [Flags]
        private enum ModuleState : sbyte
        {
            Hollow = 0,
            Instantiated = 1,
            Initialized = 2,
            Ran = 4,
            Terminated = -1,
        }
    }
}