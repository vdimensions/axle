using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.SqlClient
{
    /// <summary>
    /// An attribute that causes the annotated module to establish a dependency to the
    /// <see cref="SqlServiceProvider"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class RequiresSqlClientAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresSqlClientAttribute"/> class.
        /// </summary>
        public RequiresSqlClientAttribute() : base(typeof(SqlClientModule)) { }
    }
}