using System;
using System.Data;
using System.Data.Common;


namespace Axle.Data.Extensions.DbCommand
{
    #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
    using DataSet = System.Data.DataSet;
    #endif
    using DbCommand = System.Data.Common.DbCommand;

    public static class DbCommandExtensions
    {
        #if !NETSTANDARD || NETSTANDARD2_0_OR_NEWER
        private static int FillDataSet(this DbCommand command, DataSet ds, string tableName, Func<DbDataAdapter> createDataAdapter)
        {
            using (var da = createDataAdapter())
            {
                da.SelectCommand = command;
                try
                {
                    return da.Fill(ds, tableName);
                }
                catch
                {
                    ds.Dispose();
                    throw;
                }
            }
        }
        public static DataSet FillDataSet(this DbCommand command, string tableName, Func<DbDataAdapter> createDataAdapter)
        {
            var ds = new DataSet();
            FillDataSet(command, ds, tableName, createDataAdapter);
            return ds;
        }
        public static DataSet FillDataSet(this DbCommand command, string dataSetName, string tableName, Func<DbDataAdapter> createDataAdapter)
        {
            var ds = new DataSet(dataSetName);
            FillDataSet(command, ds, tableName, createDataAdapter);
            return ds;
        }
        #endif
    }
}