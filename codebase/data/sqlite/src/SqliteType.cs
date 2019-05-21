#if NETFRAMEWORK
namespace Axle.Data.Sqlite
{
    /// <summary>
    /// Taken as is from Faithlife/System.Data.SQLite library, where the type is left internal.
    /// see: https://github.com/Faithlife/System.Data.SQLite/blob/master/src/System.Data.SQLite/SQLiteColumnType.cs
    /// </summary>
    public enum SqliteType
    {
        /// <summary>
        /// All integers in SQLite default to Int64
        /// </summary>
        Integer = 1,

        /// <summary>
        /// All floating point numbers in SQLite default to double
        /// </summary>
        Double = 2,

        /// <summary>
        /// The default data type of SQLite is text
        /// </summary>
        Text = 3,

        /// <summary>
        /// Typically blob types are only seen when returned from a function
        /// </summary>
        Blob = 4
    }
}
#endif