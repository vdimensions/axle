﻿using System.Data;
using System.Data.SqlTypes;


namespace Axle.Data.SqlClient.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SqlInt32Converter : SqlDbTypeConverter<int?, SqlInt32>
    {
        public SqlInt32Converter() : base(DbType.Int32, SqlDbType.Int) { }

        protected override int? GetNotNullSourceValue(SqlInt32 value) => value.Value;
        protected override SqlInt32 GetNotNullDestinationValue(int? value) => new SqlInt32(value.Value);

        protected override bool IsNull(SqlInt32 value) => value.IsNull;

        protected override int? SourceNullEquivalent => null;
        protected override SqlInt32 DestinationNullEquivalent => SqlInt32.Null;
    }
}