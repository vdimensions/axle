#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    [Serializable]
    internal enum AddRemoveOperation : byte
    {
        Add,
        Remove
    }
}
#endif