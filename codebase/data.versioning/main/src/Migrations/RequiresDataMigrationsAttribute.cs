using System;
using Axle.Modularity;

namespace Axle.Data.Versioning.Migrations
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RequiresDataMigrationsAttribute : RequiresAttribute
    {
        public RequiresDataMigrationsAttribute() : base(typeof(MigratorModule)) { }
    }
}