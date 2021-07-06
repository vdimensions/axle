using System;


namespace Axle.Application
{
    partial class Application
    {
        [Flags]
        internal enum ModuleStates : sbyte
        {
            Hollow = 0,
            Instantiated = 1,
            Initialized = 2,
            Prepared = 4,
            Ran = 8,
            Terminated = -1,
        }
    }
}