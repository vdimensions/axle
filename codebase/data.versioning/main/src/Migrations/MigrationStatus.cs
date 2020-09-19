using System;

 namespace Axle.Data.Versioning.Migrations
{
    [Serializable]
    public enum MigrationStatus : byte
    {
        NotRan = 0,
        Successful = 1,
        Failed = 2,
    }
}