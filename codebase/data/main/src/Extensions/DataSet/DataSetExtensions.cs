#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Linq;

using Axle.Data.Extensions.DataTable;
using Axle.Verification;


namespace Axle.Data.Extensions.DataSet
{
    using DataSet = System.Data.DataSet;
    using DataTable = System.Data.DataTable;

    public static class DataSetExtensions
    {
        public static bool IsEmpty(this DataSet dataSet)
        {
            return dataSet.VerifyArgument(nameof(dataSet)).IsNotNull().Value.Tables.Cast<DataTable>().All(table => table.IsEmpty());
        }
    }
}
#endif