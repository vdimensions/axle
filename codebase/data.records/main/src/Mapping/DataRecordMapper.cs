#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Axle.Conversion;
using Axle.Data.Records.Mapping.Accessors;
using Axle.Data.Records.Mapping.Accessors.DataFields;
using Axle.Data.Records.Mapping.Accessors.ObjectMembers;
using Axle.Verification;

namespace Axle.Data.Records.Mapping
{
    /// <summary>
    /// An abstract class to serve as a base for implementing a custom data record mapper.
    /// The data record mapper is responsible for converting a <see cref="DataRecord"/> object
    /// to an instance of type <typeparamref name="T"/> and vice-versa.
    /// </summary>
    /// <typeparam name="T">
    /// The mapped type.
    /// </typeparam>
    public abstract class DataRecordMapper<T> : AbstractTwoWayConverter<T, DataRecord>
        where T: class
    {
        private interface IDataRecordManipulator
        {
            void UpdateObject(DataRecord dataRow, string fieldNameFormat, T obj);
            void UpdateRecord(DataRecord dataRow, string fieldNameFormat, T value);
            
            string RecordFieldName { get; }
            Type FieldType { get; }
        }

        private sealed class DataRecordManipulator<TField> : IDataRecordManipulator
        {
            private readonly IDataFieldAccessor<TField> _fieldAccessor;
            private readonly IObjectMemberAccessor<T, TField> _memberAccessor;

            public DataRecordManipulator(
                string name, 
                IDataFieldAccessor<TField> fieldAccessor, 
                IObjectMemberAccessor<T, TField> memberAccessor)
            {
                RecordFieldName = name;
                _fieldAccessor = fieldAccessor;
                _memberAccessor = memberAccessor;
                FieldType = _fieldAccessor is IFieldTypeOverride o ? o.OverridenFieldType : typeof(TField);
            }

            public void UpdateObject(DataRecord record, string fieldNameFormat, T obj)
            {
                #if NETSTANDARD || NET40_OR_NEWER
                var key = string.IsNullOrWhiteSpace(fieldNameFormat)
                #else
                var key = string.IsNullOrEmpty(fieldNameFormat) || System.Linq.Enumerable.All(fieldNameFormat, char.IsWhiteSpace)
                #endif
                    ? RecordFieldName 
                    : string.Format(fieldNameFormat, RecordFieldName);
                var value = _fieldAccessor.GetValue(record, key);
                _memberAccessor.SetValue(obj, value);
            }

            public void UpdateRecord(DataRecord record, string fieldNameFormat, T value)
            {
                #if NETSTANDARD || NET40_OR_NEWER
                var key = string.IsNullOrWhiteSpace(fieldNameFormat)
                #else
                var key = string.IsNullOrEmpty(fieldNameFormat) || System.Linq.Enumerable.All(fieldNameFormat, char.IsWhiteSpace)
                #endif
                    ? RecordFieldName 
                    : string.Format(fieldNameFormat, RecordFieldName);
                _fieldAccessor.SetValue(record, key, _memberAccessor.GetValue(value));
            }

            public string RecordFieldName { get; }
            public Type FieldType { get; }
        }
        
        private readonly IDictionary<string, IDataRecordManipulator> _fieldMappers 
            = new Dictionary<string, IDataRecordManipulator>(StringComparer.Ordinal);

        protected abstract T CreateObject();

        protected virtual DataRecord CreateRecord()
        {
            var table = new DataTable();
            foreach (var fieldMapper in _fieldMappers.Values)
            {
                table.Columns.Add(new DataColumn(fieldMapper.RecordFieldName, fieldMapper.FieldType));
            }
            return DataRecord.FromDataRow(table.NewRow());
        }
        
        protected internal void RegisterFieldAccessor<TField>(
            string name, 
            IDataFieldAccessor<TField> dataFieldAccessor, 
            IObjectMemberAccessor<T,TField> memberAccessor)
        {
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            dataFieldAccessor.VerifyArgument(nameof(dataFieldAccessor)).IsNotNull();
            memberAccessor.VerifyArgument(nameof(memberAccessor)).IsNotNull();
            _fieldMappers[name] = new DataRecordManipulator<TField>(name, dataFieldAccessor, memberAccessor);
        }
        protected internal void RegisterFieldAccessor<TField>(
                string name, 
                IDataFieldAccessor<TField> dataFieldAccessor, 
                Expression<Func<T, TField>> expression) 
            => RegisterFieldAccessor(name, dataFieldAccessor, ReflectedMemberAccessor.Create(expression));
        

        public virtual T ConvertBack(T obj, DataRecord record, string fieldNameFormat)
        {
            var result = obj;
            foreach (var converter in _fieldMappers)
            {
                converter.Value.UpdateObject(record, fieldNameFormat, result);
            }
            return result;
        }
        public T ConvertBack(T obj, DataRecord record) => ConvertBack(obj, record, null);
        public T ConvertBack(DataRecord record, string fieldNameFormat) 
            => ConvertBack(CreateObject(), record, fieldNameFormat);

        public virtual DataRecord Convert(DataRecord record, T obj, string fieldNameFormat)
        {
            foreach (var converter in _fieldMappers)
            {
                converter.Value.UpdateRecord(record, fieldNameFormat, obj);
            }
            return record;
        }
        public DataRecord Convert(DataRecord record, T obj) => Convert(record, obj, null);
        public DataRecord Convert(T obj, string fieldNameFormat) => Convert(CreateRecord(), obj, fieldNameFormat);

        protected override DataRecord DoConvert(T source) => Convert(CreateRecord(), source);
        protected override T DoConvertBack(DataRecord source) => ConvertBack(source, null);
    }
}
#endif