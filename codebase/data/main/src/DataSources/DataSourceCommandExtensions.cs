using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Axle.Conversion;
using Axle.Data.Common;
using Axle.Data.Conversion;
using Axle.Reflection;
using Axle.Verification;

namespace Axle.Data.DataSources
{
    /// <summary>
    /// A <see langword="static"/> class containing extension methods for 
    /// the <see cref="IDataSourceCommand"/> interface.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class DataSourceCommandExtensions
    {
        private static T SafeCast<T>(object obj)
        {
            var targetType = new TypeIntrospector<T>();
            var resultType = new TypeIntrospector(obj.GetType());
            if (resultType.Equals(targetType) || resultType.TypeCode == targetType.TypeCode)
            {
                return (T) obj;
            }
            switch (resultType.TypeCode)
            {
                default:
                    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                    return (T) Convert.ChangeType(obj, targetType.TypeCode);
                    #else
                    return (T) Convert.ChangeType(obj, targetType.IntrospectedType);
                    #endif
            }
        }
        
        
        
        public static T ExecuteScalar<T>(this IDataSourceCommand command, IDataSourceConnection connection, params IDataParameter[] parameters)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(command, nameof(command)));
            var result = command.ExecuteScalar(connection, parameters);
            return SafeCast<T>(result);
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
