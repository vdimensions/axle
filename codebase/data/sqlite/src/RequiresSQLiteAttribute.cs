using System;
using Axle.Modularity;

namespace Axle.Data.SQLite
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RequiresSQLiteAttribute : RequiresAttribute
    {
        public RequiresSQLiteAttribute() : base(typeof(SQLiteModule)) { }
    }
}