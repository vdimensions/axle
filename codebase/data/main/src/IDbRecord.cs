using System;
using System.Collections.Generic;


namespace Axle.Data
{
    /// <summary>
    /// An interface that represents a data record
    /// </summary>
    public interface IDbRecord : IEnumerable<object>
    {
        string GetFieldName(int i);

        Type GetFieldType(int i);

        Guid GetGuid(int i);
        Guid GetGuid(string name);

        string GetString(int i);
        string GetString(string name);

        char GetChar(int i);
        char GetChar(string name);

        bool GetBoolean(int i);
        bool GetBoolean(string name);

        byte GetByte(int i);
        byte GetByte(string name);

        short GetInt16(int i);
        short GetInt16(string name);

        int GetInt32(int i);
        int GetInt32(string name);

        long GetInt64(int i);
        long GetInt64(string name);

        float GetFloat(int i);
        float GetFloat(string name);

        double GetDouble(int i);
        double GetDouble(string name);

        decimal GetDecimal(int i);
        decimal GetDecimal(string name);

        DateTime GetDateTime(int i);
        DateTime GetDateTime(string name);

        int GetOrdinal(string name);

        /// <summary>
        /// Gets the number of columns in the current record.
        /// </summary>
        /// <returns>
        /// When not positioned in a valid recordset, <c>0</c>; otherwise, the number of columns in the current record. The default is <c>-1</c>.
        /// </returns>
        int FieldCount { get; }
        string[] FieldNames { get; }

        object this[int i] { get; }
        object this[string name] { get; }
    }
}