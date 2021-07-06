using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.SQLite
{
    /// <summary>
    /// An attribute that causes the annotated module to establish a dependency to the
    /// <see cref="SQLiteServiceProvider"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class RequiresSQLiteAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresSQLiteAttribute"/> class.
        /// </summary>
        public RequiresSQLiteAttribute() : base(typeof(SQLiteModule)) { }
    }
}