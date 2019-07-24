using Axle.Verification;

#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
namespace Axle.Data.Extensions.DataTable
{
    using DataTable = System.Data.DataTable;

    public static class DataTableExtensions
    {
        public static bool IsEmpty(this DataTable dataTable) => dataTable.VerifyArgument(nameof(dataTable)).IsNotNull().Value.Rows.Count == 0;
    }
}
#endif