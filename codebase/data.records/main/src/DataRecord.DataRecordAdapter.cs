using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Axle.Verification;

namespace Axle.Data.Records
{
    public sealed partial class DataRecord
    {
        private sealed class DataRecordAdapter : IDataRecord
        {
            private readonly System.Data.IDataRecord _target;
            private readonly IList<object> _valueOverrides;

            public DataRecordAdapter(System.Data.IDataRecord target)
            {
                _target = target.VerifyArgument(nameof(target)).IsNotNull().Value;
                _valueOverrides = new List<object>(FieldCount);
            }

            private object GetField<T>(int i)
            {
                if (_target.IsDBNull(i))
                {
                    return null;
                }
                var typeCode = Type.GetTypeCode(typeof(T));
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                        return  _target.GetBoolean(i);
                    case TypeCode.Byte:
                        return  _target.GetByte(i);
                    case TypeCode.Char:
                        return  _target.GetChar(i);
                    case TypeCode.Int16:
                        return  _target.GetInt16(i);
                    case TypeCode.Int32:
                        return  _target.GetInt32(i);
                    case TypeCode.Int64:
                        return  _target.GetInt64(i);
                    case TypeCode.Single:
                        return  _target.GetString(i);
                    case TypeCode.Double:
                        return  _target.GetDouble(i);
                    case TypeCode.Decimal:
                        return  _target.GetDecimal(i);
                    case TypeCode.DateTime:
                        return  _target.GetDateTime(i);
                    case TypeCode.String:
                        return  _target.GetString(i);
                    default:
                        var value = _target.GetValue(i);
                        if (value is T t)
                        {
                            return t;
                        }
                        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                        try
                        {
                            return Convert.ChangeType(value, typeof(T));
                        }
                        catch (InvalidCastException)
                        {
                            return Convert.ChangeType(value, typeCode);
                        }
                        #else
                        return Convert.ChangeType(value, typeof(T));
                        #endif
                }
            }

            public string GetName(int i) => _target.GetName(i);

            public Type GetType(int i) => _target.GetFieldType(i);

            public int GetOrdinal(string name) => _target.GetOrdinal(name);
            
            public T GetValue<T>(int i) => (T) GetField<T>(i);
            public T GetValue<T>(string name) => (T) GetField<T>(GetOrdinal(name.VerifyArgument(nameof(name)).IsNotNullOrEmpty()));
            
            public void SetValue<T>(int i, T value) => _valueOverrides[i] = value;
            public void SetValue<T>(string name, T value) => SetValue(_target.GetOrdinal(name), value);

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

            public object this[int i]
            {
                get => _target[i];
                set
                {
                    if (i < 0)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    _valueOverrides[i] = value;
                }
            }

            public object this[string name]
            {
                get
                {
                    name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
                    try
                    {
                        var indexOfName = GetOrdinal(name);
                        if (indexOfName < 0)
                        {
                            throw new IndexOutOfRangeException();
                        }
                        return _valueOverrides[indexOfName] ?? _target[name];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return null;
                    }
                }
                set => this[GetOrdinal(name)] = value;
            }
        }
        
        public static DataRecord FromDataRecord(DbDataRecord dataRecord) =>
            new DataRecord(new DataRecordAdapter(dataRecord.VerifyArgument(nameof(dataRecord)).IsNotNull().Value));

        public static DataRecord FromDataReader(DbDataReader dataReader) =>
            new DataRecord(new DataRecordAdapter(dataReader.VerifyArgument(nameof(dataReader)).IsNotNull().Value));
        
        public static implicit operator DataRecord(DbDataRecord dataRecord) => FromDataRecord(dataRecord);

        public static implicit operator DataRecord(DbDataReader dataReader) => FromDataReader(dataReader);
    }
}