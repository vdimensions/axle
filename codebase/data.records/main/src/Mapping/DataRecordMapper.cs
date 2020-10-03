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
            public Type FieldType => typeof(T);
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
        
        protected void RegisterFieldAccessor<TField>(
            string name, 
            IDataFieldAccessor<TField> dataFieldAccessor, 
            IObjectMemberAccessor<T,TField> memberAccessor)
        {
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            dataFieldAccessor.VerifyArgument(nameof(dataFieldAccessor)).IsNotNull();
            memberAccessor.VerifyArgument(nameof(memberAccessor)).IsNotNull();
            _fieldMappers[name] = new DataRecordManipulator<TField>(name, dataFieldAccessor, memberAccessor);
        }
        protected void RegisterFieldAccessor<TField>(
                string name, 
                IDataFieldAccessor<TField> dataFieldAccessor, 
                Expression<Func<T, TField>> expression) 
            => RegisterFieldAccessor(name, dataFieldAccessor, ReflectedMemberAccessor.Create(expression));
        protected void RegisterFieldAccessor(string name, Expression<Func<T, bool>> expression)
            => RegisterFieldAccessor(name, new BooleanDataFieldAccessor(), expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, char>> expression)
            => RegisterFieldAccessor(name, new CharacterDataFieldAccessor(), expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, byte>> expression)
            => RegisterFieldAccessor(name, new ByteDataFieldAccessor(), expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, short>> expression)
            => RegisterFieldAccessor(name, new Int16DataFieldAccessor(), expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, int>> expression)
            => RegisterFieldAccessor(name, new Int32DataFieldAccessor(), expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, long>> expression)
            => RegisterFieldAccessor(name, new Int64DataFieldAccessor(), expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, DateTime>> expression)
            => RegisterFieldAccessor(
                name, 
                new Int64DataFieldAccessor().Transform(new DateTimeToTicksConverter(DateTimeKind.Utc).Invert()), 
                expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, TimeSpan>> expression)
            => RegisterFieldAccessor(
                name, 
                new Int64DataFieldAccessor().Transform(new TimeSpanToTicksConverter().Invert()), 
                expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, float>> expression)
            => RegisterFieldAccessor(name, new SingleDataFieldAccessor(), expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, double>> expression)
            => RegisterFieldAccessor(name, new DoubleDataFieldAccessor(), expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, decimal>> expression)
            => RegisterFieldAccessor(name, new DecimalDataFieldAccessor(), expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, string>> expression)
            => RegisterFieldAccessor(name, new StringDataFieldAccessor(), expression);
        protected void RegisterFieldAccessor(string name, Expression<Func<T, Guid>> expression)
            => RegisterFieldAccessor(name, new GuidDataFieldAccessor(), expression);

        public virtual T Convert(DataRecord record, string fieldNameFormat)
        {
            var result = CreateObject();
            foreach (var converter in _fieldMappers)
            {
                converter.Value.UpdateObject(record, fieldNameFormat, result);
            }
            return result;
        }

        public virtual DataRecord ConvertBack(DataRecord record, T obj, string fieldNameFormat)
        {
            foreach (var converter in _fieldMappers)
            {
                converter.Value.UpdateRecord(record, fieldNameFormat, obj);
            }
            return record;
        }
        public DataRecord ConvertBack(DataRecord record, T obj) => ConvertBack(record, obj, null);
        public DataRecord ConvertBack(T obj, string fieldNameFormat) => ConvertBack(CreateRecord(), obj, fieldNameFormat);

        protected override DataRecord DoConvert(T source) => ConvertBack(CreateRecord(), source);
        protected override T DoConvertBack(DataRecord source) => Convert(source, null);
    }
}
#endif