using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Axle.Verification;


namespace Axle.Data.Common
{
    internal sealed class DataRecordAdapter : IDbRecord
    {
        private readonly IDataRecord _target;

        public DataRecordAdapter(IDataRecord target)
        {
            _target = target.VerifyArgument(nameof(target)).IsNotNull().Value;
        }

        public string GetFieldName(int i) => _target.GetName(i);

        public Type GetFieldType(int i) => _target.GetFieldType(i);

        public Guid GetGuid(int i) => _target.GetGuid(i);
        public Guid GetGuid(string name) => _target.GetGuid(_target.GetOrdinal(name));

        public string GetString(int i) => _target.GetString(i);
        public string GetString(string name) => _target.GetString(_target.GetOrdinal(name));

        public char GetChar(int i) => _target.GetChar(i);
        public char GetChar(string name) => _target.GetChar(_target.GetOrdinal(name));

        public bool GetBoolean(int i) => _target.GetBoolean(i);
        public bool GetBoolean(string name) => _target.GetBoolean(_target.GetOrdinal(name));

        public byte GetByte(int i) => _target.GetByte(i);
        public byte GetByte(string name) => _target.GetByte(_target.GetOrdinal(name));

        public short GetInt16(int i) => _target.GetInt16(i);
        public short GetInt16(string name) => _target.GetInt16(_target.GetOrdinal(name));

        public int GetInt32(int i) => _target.GetInt32(i);
        public int GetInt32(string name) => _target.GetInt32(_target.GetOrdinal(name));

        public long GetInt64(int i) => _target.GetInt64(i);
        public long GetInt64(string name) => _target.GetInt64(_target.GetOrdinal(name));

        public float GetSingle(int i) => _target.GetFloat(i);
        public float GetSingle(string name) => _target.GetFloat(_target.GetOrdinal(name));

        public double GetDouble(int i) => _target.GetDouble(i);
        public double GetDouble(string name) => _target.GetDouble(_target.GetOrdinal(name));

        public decimal GetDecimal(int i) => _target.GetDecimal(i);
        public decimal GetDecimal(string name) => _target.GetDecimal(_target.GetOrdinal(name));

        public DateTime GetDateTime(int i) => _target.GetDateTime(i);
        public DateTime GetDateTime(string name) => _target.GetDateTime(_target.GetOrdinal(name));

        public int GetOrdinal(string name) => _target.GetOrdinal(name);

        public IEnumerator<object> GetEnumerator()
        {
            for (var index = 0; index < _target.FieldCount; index++ )
            {
                yield return _target[index];
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int FieldCount => _target.FieldCount;

        public string[] FieldNames
        {
            get
            {
                var c = FieldCount;
                var result = new string[c];
                for (var i = 0; i < c; i++)
                {
                    result[i] = _target.GetName(i);
                }
                return result;
            }
        }

        public object this[int i] => _target[i];

        public object this[string name]
        {
            get
            {
                try
                {
                    return _target[name.VerifyArgument(nameof(name)).IsNotNull()];
                }
                catch (IndexOutOfRangeException)
                {
                    return null;
                }
            }
        }
    }
}