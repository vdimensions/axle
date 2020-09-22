#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Data;
using System.Data.Common;
using Axle.Verification;
#endif

namespace Axle.Data.Extensions.DbCommand
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    using DataSet = System.Data.DataSet;
    #endif
    using DbCommand = System.Data.Common.DbCommand;
    using DataTable = System.Data.DataTable;

    public static class DbCommandExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        internal static int FillDataSet(this IDbCommand command, DataSet ds, Func<IDbDataAdapter> createDataAdapter)
        {
            var da = createDataAdapter();
            var disposable = da as IDisposable;

            try
            {
                da.SelectCommand = command;
                try
                {
                    return da.Fill(ds);
                }
                catch
                {
                    ds.Dispose();
                    throw;
                }
            }
            finally
            {
                disposable?.Dispose();
            }
        }
        internal static int FillDataSet(this DbCommand command, DataSet ds, string tableName, Func<DbDataAdapter> createDataAdapter)
        {
            using (var da = createDataAdapter())
            {
                da.SelectCommand = command.VerifyArgument(nameof(command)).IsNotNull();;
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

        internal static int FillDataTable(this DbCommand command, DataTable dt, Func<DbDataAdapter> createDataAdapter)
        {
            using (var da = createDataAdapter())
            {
                da.SelectCommand = command.VerifyArgument(nameof(command)).IsNotNull();
                try
                {
                    return da.Fill(dt);
                }
                catch
                {
                    dt.Dispose();
                    throw;
                }
            }
        }
        #endif
    }
}