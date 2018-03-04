#if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
using Axle.Data.Extensions.DataTable;


namespace Axle.Data.Extensions.DataSet
{
    using DataSet = System.Data.DataSet;
    using DataTable = System.Data.DataTable;

    public static class DataSetExtensions
    {
        public static bool IsEmpty(this DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                if (!table.IsEmpty())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
#endif