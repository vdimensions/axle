﻿using System.Data;

using NpgsqlTypes;


namespace Axle.Data.Npgsql.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class NpgsqlDoubleConverter : NpgsqlSameTypeConverter<double?>
    {
        public NpgsqlDoubleConverter() : base(DbType.Double, NpgsqlDbType.Double, true) { }
    }
}