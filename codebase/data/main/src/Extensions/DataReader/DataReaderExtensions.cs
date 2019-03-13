using System;
using System.Data;


namespace Axle.Data.Extensions.DataReader
{
    public static class DataReaderExtensions
    {
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static object GetData(IDataRecord reader, int columnIndex) => reader[columnIndex];
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static object GetData(IDataRecord reader, string columnName) => reader[columnName];

        public static T Fetch<T>(this IDataReader reader, int columnIndex) => (T) GetData(reader, columnIndex);
        public static T Fetch<T>(this IDataReader reader, string columnName) => (T) GetData(reader, columnName);

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        internal static bool TryCast<T>(object obj, out T? result) where T: struct => (result = obj as T?) != null;
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        internal static bool TryCast<T>(object obj, out T result)
        {
            try
            {
                result = (T)obj;
                return true;
            }
            catch
            {
                try
                {
                    result = (T)Convert.ChangeType(obj, typeof(T));
                    return true;
                }
                catch
                {
                    result = default(T);
                    return false;
                }
            }
        }

        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        private static bool TryGetData(IDataRecord reader, int columnIndex, out object result)
        {
            if (columnIndex > 0 && reader.FieldCount > columnIndex)
            {
                result = reader[columnIndex];
                return true;
            }
            result = null;
            return false;
        }

        //NOT PERFORMANCE FRIENDLY
        //
        //private static bool TryGetData(IDbRecord reader, string columnName, out object result)
        //{
        //    try
        //    {
        //        result = reader[columnName];
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        result = null;
        //        return false;
        //    }
        //}

        public static bool TryFetch<T>(this IDataReader reader, int columnIndex, out T result)
        {
            if (!TryGetData(reader, columnIndex, out var res) || res == DBNull.Value || !TryCast(res, out result))
            {
                result = default(T);
                return false;
            }
            return true;
        }
        public static bool TryFetch<T>(this IDataReader reader, int columnIndex, out T? result) where T : struct
        {
            result = null;
            return TryGetData(reader, columnIndex, out var res) && TryFetchValueType(res, out result);
        }

        //LOW PERFORMANCE
        //
        //public static bool TryFetch<T>(this IDataReader reader, string columnName, out T? result) where T: struct
        //{
        //    var res = GetData(reader, columnName);
        //    return TryFetch(res, out result);
        //}
        // LOW PERFORMANCE
        //
        //public static bool TryFetch<T>(this IDataReader reader, string columnName, out T result)
        //{
        //    var res = GetData(reader, columnName);
        //    if (res.GetType().IsAssignableFrom<T>())
        //    {
        //        result = res != DBNull.Value ? res.Cast<T>() : default(T);
        //        return true;
        //    }
        //    result = default(T);
        //    return false;
        //}
        #if NETSTANDARD || NET45_OR_NEWER
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        internal static bool TryFetchValueType<T>(object res, out T? result) where T : struct
        {
            if (res != DBNull.Value)
            {
                return TryCast(res, out result);
            }
            result = null;
            return false;
        }
    }
}
