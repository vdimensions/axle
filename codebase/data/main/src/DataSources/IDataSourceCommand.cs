using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Axle.Data.DataSources
{
    public interface IDataSourceCommand : IDataSourceObject
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        void ExecuteReader(IDataSourceConnection connection, CommandBehavior behavior, Action<DbDataReader> readAction, params IDataParameter[] parameters);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        int ExecuteNonQuery(IDataSourceConnection connection, params IDataParameter[] parameters);

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        int ExecuteQuery(IDataSourceConnection connection, DataSet results, params IDataParameter[] parameters);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        int ExecuteQuery(IDataSourceConnection connection, DataTable results, params IDataParameter[] parameters);
        #endif

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        object ExecuteScalar(IDataSourceConnection connection, params IDataParameter[] parameters);
    }
}