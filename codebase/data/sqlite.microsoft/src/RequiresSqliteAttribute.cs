using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.Sqlite.Microsoft
{
    /// <summary>
    /// An attribute that causes the annotated module to establish a dependency to the
    /// <see cref="SqliteServiceProvider"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class RequiresSqliteAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresSqliteAttribute"/> class.
        /// </summary>
        public RequiresSqliteAttribute() : base(typeof(SqliteServiceProvider)) { }
    }
}