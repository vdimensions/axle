using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Axle.Conversion;
using Axle.Data.Common;
using Axle.Data.Conversion;
using Axle.Verification;


namespace Axle.Data.DataSources
{
    /// <summary>
    /// A static class containing extension methods for <see cref="IDataSourceCommand"/> instances.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class DataSourceCommandExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static T[] ExecuteQuery<T>(this IDataSourceCommand command, IDataSourceConnection connection, DataTable results, IConverter<IDbRecord, T> converter, params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(command)));
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter)));
            var result = new T[command.ExecuteQuery(connection, results, parameters)];
            var rows = results.Rows;
            for (var i = 0; i < rows.Count; i++)
            {
                var record = new DataRowAdapter(rows[i]);
                result[i] = converter.Convert(record);
            }
            return result;
        }
        public static T[] ExecuteQuery<T>(this IDataSourceCommand command, IDataSourceConnection connection, IDbRecordConverter<T> converter, params IDataParameter[] parameters) => ExecuteQuery(command, connection, new DataTable(), converter, parameters);
        public static T[] ExecuteQuery<T>(this IDataSourceCommand command, IDataSourceConnection connection, DataTable results, Converter<IDbRecord, T> converter, params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(command)));
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter)));
            var result = new T[command.ExecuteQuery(connection, results, parameters)];
            var rows = results.Rows;
            for (var i = 0; i < rows.Count; i++)
            {
                var record = new DataRowAdapter(rows[i]);
                result[i] = converter(record);
            }
            return result;
        }
        public static T[] ExecuteQuery<T>(this IDataSourceCommand command, IDataSourceConnection connection, Converter<IDbRecord, T> converter, params IDataParameter[] parameters) => ExecuteQuery(command, connection, new DataTable(), converter, parameters);
        #endif
        
        public static void ExecuteReader(this IDataSourceCommand command, IDataSourceConnection connection, Action<DbDataReader> readAction, params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(command, nameof(command)));
            command.ExecuteReader(connection, CommandBehavior.Default, readAction, parameters);
        }
        public static IEnumerable<T> ExecuteReader<T>(this IDataSourceCommand command, IDataSourceConnection connection, IDbRecordConverter<T> converter, params IDataParameter[] parameters)
        {
            return ExecuteReader(command, connection, CommandBehavior.Default, converter, parameters);
        }
        public static IEnumerable<T> ExecuteReader<T>(this IDataSourceCommand command, IDataSourceConnection connection, IConverter<IDbRecord, T> converter, params IDataParameter[] parameters)
        {
            return ExecuteReader(command, connection, CommandBehavior.Default, converter, parameters);
        }
        public static IEnumerable<T> ExecuteReader<T>(this IDataSourceCommand command, IDataSourceConnection connection, CommandBehavior commandBehavior, IConverter<IDbRecord, T> converter, params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(command, nameof(command)));
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter)));
            ICollection<T> result = new LinkedList<T>();
            command.ExecuteReader(connection, commandBehavior, r => result.Add(converter.Convert(new DataRecordAdapter(r))), parameters);
            return result.ToArray();
        }
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static IEnumerable<T> ExecuteReader<T>(this IDataSourceCommand command, IDataSourceConnection connection, CommandBehavior commandBehavior, Converter<IDbRecord, T> converter, params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(command, nameof(command)));
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter)));
            ICollection<T> result = new LinkedList<T>();
            command.ExecuteReader(connection, commandBehavior, r => result.Add(converter(new DataRecordAdapter(r))), parameters);
            return result.ToArray();
        }
        public static IEnumerable<T> ExecuteReader<T>(this IDataSourceCommand command, IDataSourceConnection connection, Converter<IDbRecord, T> converter, params IDataParameter[] parameters)
        {
            return ExecuteReader(command, connection, CommandBehavior.Default, converter, parameters);
        }
        #endif
        
        public static T ExecuteScalar<T>(this IDataSourceCommand command, IDataSourceConnection connection, params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(command, nameof(command)));
            return (T) command.ExecuteScalar(connection, parameters);
        }
        public static TResult ExecuteScalar<T, TResult>(this IDataSourceCommand command, IDataSourceConnection connection, IConverter<T, TResult> converter, params IDataParameter[] parameters)
        {
            converter.VerifyArgument(nameof(converter)).IsNotNull();
            return converter.Convert(ExecuteScalar<T>(command, connection, parameters));
        }
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static TResult ExecuteScalar<T, TResult>(this IDataSourceCommand command, IDataSourceConnection connection, Converter<T, TResult> converter, params IDataParameter[] parameters)
        {
            converter.VerifyArgument(nameof(converter)).IsNotNull();
            return converter(ExecuteScalar<T>(command, connection, parameters));
        }
        #endif

    }
}
