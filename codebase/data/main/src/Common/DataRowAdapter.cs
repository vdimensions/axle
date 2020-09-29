#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Axle.Verification;


namespace Axle.Data.Common
{
    internal sealed class DataRowAdapter : IDbRecord
    {
        private readonly DataRow _target;

        public DataRowAdapter(DataRow target)
        {
            _target = target.VerifyArgument(nameof(target)).IsNotNull();
        }

        public string GetFieldName(int i) => _target.Table.Columns[i].ColumnName;
        public Type GetFieldType(int i) => _target.Table.Columns[i].DataType;

        public Guid GetGuid(int i) => _target.Field<Guid>(i);
        public Guid GetGuid(string name) => _target.Field<Guid>(name);

        public string GetString(int i) => _target.Field<string>(i);
        public string GetString(string name) => _target.Field<string>(name);

        public char GetChar(int i) => _target.Field<char>(i);
        public char GetChar(string name) => _target.Field<char>(name);

        public bool GetBoolean(int i) => _target.Field<bool>(i);
        public bool GetBoolean(string name) => _target.Field<bool>(name);

        public byte GetByte(int i) => _target.Field<byte>(i);
        public byte GetByte(string name) => _target.Field<byte>(name);

        public short GetInt16(int i) => _target.Field<short>(i);
        public short GetInt16(string name) => _target.Field<short>(name);

        public int GetInt32(int i) => _target.Field<int>(i);
        public int GetInt32(string name) => _target.Field<int>(name);

        public long GetInt64(int i) => _target.Field<long>(i);
        public long GetInt64(string name) => _target.Field<long>(name);

        public float GetSingle(int i) => _target.Field<float>(i);
        public float GetSingle(string name) => _target.Field<float>(name);

        public double GetDouble(int i) => _target.Field<double>(i);
        public double GetDouble(string name) => _target.Field<double>(name);

        public decimal GetDecimal(int i) => _target.Field<decimal>(i);
        public decimal GetDecimal(string name) => _target.Field<decimal>(name);

        public DateTime GetDateTime(int i) => _target.Field<DateTime>(i);
        public DateTime GetDateTime(string name) => _target.Field<DateTime>(name);

        public int GetOrdinal(string name) => _target.Table.Columns[name].Ordinal;

        public IEnumerator<object> GetEnumerator() => _target.ItemArray.Cast<object>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int FieldCount => _target.ItemArray.Length;
        public string[] FieldNames => _target.Table.Columns.OfType<DataColumn>().Select(x => x.ColumnName).ToArray();

        public object this[int i] => _target[i];

        public object this[string name]
        {
            get
            {
                try
                {
                    return _target[name];
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
        }
    }
}
#endif
