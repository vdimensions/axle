using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Data
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class DbParameterValueBuilderExtensions
    {
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, string value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.String, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, char value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.StringFixedLength, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, bool value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.Boolean, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, byte value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.Byte, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, sbyte value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.SByte, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, short value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.Int16, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, ushort value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.UInt16, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, int value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.Int32, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, uint value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.UInt32, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, long value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.Int64, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, ulong value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.UInt64, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, float value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.Single, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, double value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.Double, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, decimal value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.Decimal, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, Guid value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.Guid, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, DateTime value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.DateTime, value);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, byte[] value)
        {
            return builder.VerifyArgument("builder").IsNotNull().Value.SetValue(DbType.Binary, value);
        }
    }
}