#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;

using Axle.Data.Extensions.DataReader;


namespace Axle.Data.Extensions.DataRow
{
    using DataRow = System.Data.DataRow;

    public static class DataRowExtensions
    {
        public static T Fetch<T>(this DataRow row, int columnIndex) => (T) GetData(row, columnIndex);
        public static T Fetch<T>(this DataRow row, string columnName) => (T) GetData(row, columnName);

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static object GetData(DataRow row, int columnIndex) { return row[columnIndex]; }
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static object GetData(DataRow row, string columnName) { return row[columnName]; }

        public static bool TryFetch<T>(this DataRow row, int columnIndex, out T result)
        {
            if (!TryGetData(row, columnIndex, out var res) || res == DBNull.Value || !DataReaderExtensions.TryCast(res, out result))
            {
                result = default(T);
                return false;
            }
            return true;
        }
        public static bool TryFetch<T>(this DataRow row, string columnName, out T result)
        {
            if (!TryGetData(row, columnName, out var res) || res == DBNull.Value || !DataReaderExtensions.TryCast(res, out result))
            {
                result = default(T);
                return false;
            }
            return true;
        }
        public static bool TryFetch<T>(this DataRow row, int columnIndex, out T? result) where T : struct
        {
            result = null;
            return TryGetData(row, columnIndex, out var res) && DataReaderExtensions.TryFetchValueType(res, out result);
        }
        public static bool TryFetch<T>(this DataRow row, string columnName, out T? result) where T : struct
        {
            result = null;
            return TryGetData(row, columnName, out var res) && DataReaderExtensions.TryFetchValueType(res, out result);
        }

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static bool TryGetData(DataRow row, int columnIndex, out object result)
        {
            if (columnIndex > 0 && row.Table.Columns.Count > columnIndex)
            {
                result = row[columnIndex];
                return true;
            }
            result = null;
            return false;
        }
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static bool TryGetData(DataRow row, string columnName, out object result)
        {
            if (!string.IsNullOrEmpty(columnName) && row.Table.Columns.Contains(columnName))
            {
                result = row[columnName];
                return true;
            }
            result = null;
            return false;
        }
    }
}
#endif