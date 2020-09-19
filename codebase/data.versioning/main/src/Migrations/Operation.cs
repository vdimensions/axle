using System;

namespace Axle.Data.Versioning.Migrations
{
    [Serializable]
    public enum Operation : byte
    {
        Apply = 0,
        Revert = 1
    }
}