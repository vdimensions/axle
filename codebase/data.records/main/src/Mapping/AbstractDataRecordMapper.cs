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
    public abstract class AbstractDataRecordMapper<T> where T: class
    {
        internal interface IDataRowManipulator
        {
            void GetValue(DataRecord dataRow, string fieldNameFormat, T obj);
            void SetValue(DataRecord dataRow, string fieldNameFormat, T value);
        }
        
        internal sealed class DataRowManipulator<TField> : IDataRowManipulator
        {
            private readonly IDataFieldAccessor<TField> _fieldAccessor;
            private readonly IObjectMemberAccessor<T, TField> _memberAccessor;

            public DataRowManipulator(
                string name, 
                IDataFieldAccessor<TField> fieldAccessor, 
                IObjectMemberAccessor<T, TField> memberAccessor)
            {
                Name = name;
                _fieldAccessor = fieldAccessor;
                _memberAccessor = memberAccessor;
            }

            public void GetValue(DataRecord record, string fieldNameFormat, T obj)
            {
                #if NETSTANDARD || NET40_OR_NEWER
                var key = string.IsNullOrWhiteSpace(fieldNameFormat)
                #else
                var key = string.IsNullOrEmpty(fieldNameFormat) || System.Linq.Enumerable.All(fieldNameFormat, char.IsWhiteSpace)
                #endif
                    ? Name 
                    : string.Format(fieldNameFormat, Name);
                var value = _fieldAccessor.GetValue(record, key);
                _memberAccessor.SetValue(obj, value);
            }

            public void SetValue(DataRecord record, string fieldNameFormat, T value)
            {
                #if NETSTANDARD || NET40_OR_NEWER
                var key = string.IsNullOrWhiteSpace(fieldNameFormat)
                #else
                var key = string.IsNullOrEmpty(fieldNameFormat) || System.Linq.Enumerable.All(fieldNameFormat, char.IsWhiteSpace)
                #endif
                    ? Name 
                    : string.Format(fieldNameFormat, Name);
                _fieldAccessor.SetValue(record, key, _memberAccessor.GetValue(value));
            }

            public string Name { get; }
        }
        
        private readonly IDictionary<string, IDataRowManipulator> _fieldMappers 
            = new Dictionary<string, IDataRowManipulator>(StringComparer.Ordinal);

        protected abstract T Instantiate();
        
        protected void RegisterFieldAccessor<TField>(
            string name, 
            IDataFieldAccessor<TField> dataFieldAccessor, 
            IObjectMemberAccessor<T,TField> memberAccessor)
        {
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            dataFieldAccessor.VerifyArgument(nameof(dataFieldAccessor)).IsNotNull();
            memberAccessor.VerifyArgument(nameof(memberAccessor)).IsNotNull();
            _fieldMappers[name] = new DataRowManipulator<TField>(name, dataFieldAccessor, memberAccessor);
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

        public virtual T Convert(DataRow record, string fieldNameFormat)
        {
            var result = Instantiate();
            foreach (var converter in _fieldMappers)
            {
                converter.Value.GetValue(record, fieldNameFormat, result);
            }
            return result;
        }
        public T Convert(DataRow dataRow) => Convert(dataRow, null);

        public virtual DataRow ConvertBack(DataRow record, T obj, string fieldNameFormat)
        {
            foreach (var converter in _fieldMappers)
            {
                converter.Value.SetValue(record, fieldNameFormat, obj);
            }
            return record;
        }
        public DataRow ConvertBack(DataRow record, T obj) => ConvertBack(record, obj, null);
    }
}
#endif