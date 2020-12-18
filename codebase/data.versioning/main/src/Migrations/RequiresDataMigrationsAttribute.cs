using System;
using Axle.Modularity;

namespace Axle.Data.Versioning.Migrations
{
    /// <summary>
    /// Causes the annotated target module to become dependent on the data migrator module.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RequiresDataMigrationsAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresDataMigrationsAttribute"/> class.
        /// </summary>
        public RequiresDataMigrationsAttribute() : base(typeof(MigratorModule)) { }
    }
}