#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using Axle.Verification;

namespace Axle.Data.Extensions.DataTable
{
    using DataTable = System.Data.DataTable;

    /// A static class to contain common extension methods for the <see cref="System.Data.DataTable" /> type.
    public static class DataTableExtensions
    {
        public static bool IsEmpty(this DataTable dataTable) => dataTable.VerifyArgument(nameof(dataTable)).IsNotNull().Value.Rows.Count == 0;
    }
}
#endif