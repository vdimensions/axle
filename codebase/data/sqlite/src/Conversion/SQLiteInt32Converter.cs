﻿using System.Data;


namespace Axle.Data.SQLite.Conversion
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    internal sealed class SQLiteInt32Converter : SQLiteSameTypeConverter<int?>
    {
        public SQLiteInt32Converter() : base(DbType.Int32, SQLiteType.Integer, false) { }
    }
}