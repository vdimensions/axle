using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Axle.Conversion;
using Axle.Data.DataSources;
using Axle.Data.Records.Conversion;
using Axle.Verification;

namespace Axle.Data.Records.Extensions.DataSources
{
    public static class DataSourceCommandExtensions
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static T[] ExecuteQuery<T>(
            this IDataSourceCommand command, 
            IDataSourceConnection connection, 
            DataTable results, 
            IConverter<DataRecord, T> converter, 
            params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(command)));
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter)));
            var result = new T[command.ExecuteQuery(connection, results, parameters)];
            var rows = results.Rows;
            for (var i = 0; i < rows.Count; i++)
            {
                var record = DataRecord.FromDataRow(rows[i]);
                result[i] = converter.Convert(record);
            }
            return result;
        }
        public static T[] ExecuteQuery<T>(
            this IDataSourceCommand command, 
            IDataSourceConnection connection, 
            IDataRecordConverter<T> converter, 
            params IDataParameter[] parameters)
        {
            return ExecuteQuery(command, connection, new DataTable(), converter, parameters);
        }

        public static T[] ExecuteQuery<T>(
            this IDataSourceCommand command, 
            IDataSourceConnection connection, 
            DataTable results, 
            Converter<DataRecord, T> converter, 
            params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(command)));
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter)));
            var result = new T[command.ExecuteQuery(connection, results, parameters)];
            var rows = results.Rows;
            for (var i = 0; i < rows.Count; i++)
            {
                var record = DataRecord.FromDataRow(rows[i]);
                result[i] = converter(record);
            }
            return result;
        }
        public static T[] ExecuteQuery<T>(
            this IDataSourceCommand command, 
            IDataSourceConnection connection, Converter<DataRecord, T> converter, params IDataParameter[] parameters)
        {
            return ExecuteQuery(command, connection, new DataTable(), converter, parameters);
        }
        #endif
        
        public static IEnumerable<T> ExecuteReader<T>(
            this IDataSourceCommand command, 
            IDataSourceConnection connection, 
            IDataRecordConverter<T> converter, 
            params IDataParameter[] parameters)
        {
            return ExecuteReader(command, connection, CommandBehavior.Default, converter, parameters);
        }
        public static IEnumerable<T> ExecuteReader<T>(
            this IDataSourceCommand command, 
            IDataSourceConnection connection, 
            IConverter<DataRecord, T> converter, 
            params IDataParameter[] parameters)
        {
            return ExecuteReader(command, connection, CommandBehavior.Default, converter, parameters);
        }
        public static IEnumerable<T> ExecuteReader<T>(
            this IDataSourceCommand command, 
            IDataSourceConnection connection, 
            CommandBehavior commandBehavior, 
            IConverter<DataRecord, T> converter, 
            params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(command, nameof(command)));
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter)));
            ICollection<T> result = new LinkedList<T>();
            command.ExecuteReader(connection, commandBehavior, r => result.Add(converter.Convert(DataRecord.FromDataReader(r))), parameters);
            return result.ToArray();
        }
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static IEnumerable<T> ExecuteReader<T>(
            this IDataSourceCommand command, 
            IDataSourceConnection connection, 
            CommandBehavior commandBehavior, 
            Converter<DataRecord, T> converter, 
            params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(command, nameof(command)));
            Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter)));
            ICollection<T> result = new LinkedList<T>();
            command.ExecuteReader(connection, commandBehavior, r => result.Add(converter(DataRecord.FromDataReader(r))), parameters);
            return result.ToArray();
        }
        public static IEnumerable<T> ExecuteReader<T>(
            this IDataSourceCommand command, 
            IDataSourceConnection connection, 
            Converter<DataRecord, T> converter, 
            params IDataParameter[] parameters)
        {
            return ExecuteReader(command, connection, CommandBehavior.Default, converter, parameters);
        }
        #endif
    }
}