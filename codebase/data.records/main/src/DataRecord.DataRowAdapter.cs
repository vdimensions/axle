using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Axle.Verification;

namespace Axle.Data.Records
{
    public sealed partial class DataRecord
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        private sealed class DataRowAdapter : IDataRecord
        {
            private readonly DataRow _target;

            public DataRowAdapter(DataRow target)
            {
                _target = target.VerifyArgument(nameof(target)).IsNotNull();
            }

            public IEnumerator<object> GetEnumerator() => Enumerable.Cast<object>(_target.ItemArray).GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public int GetOrdinal(string name) => _target.Table.Columns[name].Ordinal;

            public string GetName(int i) => _target.Table.Columns[i].ColumnName;
            public Type GetType(int i) => _target.Table.Columns[i].DataType;
            
            public T GetValue<T>(int i) => DataRowExtensions.Field<T>(_target, i);
            public T GetValue<T>(string name) => DataRowExtensions.Field<T>(_target, name);
            
            public void SetValue<T>(int i, T value) => DataRowExtensions.SetField(_target, i, value);
            public void SetValue<T>(string name, T value) => DataRowExtensions.SetField(_target, name, value);

            public int FieldCount => _target.ItemArray.Length;
            
            public string[] FieldNames => Enumerable.OfType<DataColumn>(_target.Table.Columns).Select(x => x.ColumnName).ToArray();

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
        
        public static DataRecord FromDataRow(System.Data.DataRow dataRow)
        {
            return new DataRecord(new DataRowAdapter(dataRow.VerifyArgument(nameof(dataRow)).IsNotNull()));
        }
        
        public static implicit operator DataRecord(DataRow dataRow) => FromDataRow(dataRow);
        #endif
    }
}