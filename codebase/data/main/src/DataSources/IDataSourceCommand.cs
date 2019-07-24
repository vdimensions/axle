using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Data.DataSources
{
    public interface IDataSourceCommand
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        void ExecuteReader(CommandBehavior behavior, Action<DbDataReader> readAction, params IDataParameter[] parameters);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        int ExecuteNonQuery(params IDataParameter[] parameters);

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        int ExecuteQuery(DataSet results, params IDataParameter[] parameters);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        int ExecuteQuery(DataTable results, params IDataParameter[] parameters);
        #endif

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        object ExecuteScalar(params IDataParameter[] parameters);
    }
}