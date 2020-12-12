using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Axle.Verification;

namespace Axle.Data
{
    /// <summary>
    /// A static class containing extension methods aiding the definition of values for db parameters
    /// via the <see cref="IDbParameterValueBuilder"/> interface.
    /// </summary>
    /// <seealso cref="IDbParameterValueBuilder"/>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class DbParameterValueBuilderExtensions
    {
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, string value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.String);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, char value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.StringFixedLength);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, bool value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.Boolean);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, byte value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.Byte);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, sbyte value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.SByte);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, short value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.Int16);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, ushort value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.UInt16);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, int value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.Int32);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, uint value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.UInt32);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, long value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.Int64);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, ulong value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.UInt64);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, float value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.Single);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, double value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.Double);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, decimal value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.Decimal);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, Guid value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.Guid);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, DateTime value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.DateTime);
        }
        public static IDbParameterOptionalPropertiesBuilder SetValue(this IDbParameterValueBuilder builder, byte[] value)
        {
            return builder.VerifyArgument(nameof(builder)).IsNotNull().Value.SetValue(value).SetType(DbType.Binary);
        }
    }
}