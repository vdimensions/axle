using System;
using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.Npgsql
{
    /// <summary>
    /// An attribute that causes the annotated module to establish a dependency to the
    /// <see cref="NpgsqlServiceProvider"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class RequiresPostgreSqlAttribute : RequiresAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresPostgreSqlAttribute"/> class.
        /// </summary>
        public RequiresPostgreSqlAttribute() : base(typeof(NpgsqlModule)) { }
    }
}