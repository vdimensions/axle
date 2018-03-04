using System;
using System.Linq.Expressions;

using Axle.Conversion;


namespace Axle.Data.Common.Conversion
{
    //public abstract class AbstractDbRecordConverter<T> : AbstractConverter<IDbRecord, T>, IDbRecordConverter<T>
    //{
    //    protected sealed class GenericDbRecord<TData> : DbRecordDecorator
    //    {
    //        internal GenericDbRecord(IDbRecord target) : base(target) { }
    //
    //        public TResult Get<TResult>(Expression<Func<TData, TResult>> expression, string fieldNameFormat)
    //        {
    //            return Target.Get(expression, fieldNameFormat);
    //        }
    //    }
    //
    //    public T Convert(IDbRecord value, string fieldNameFormat)
    //    {
    //        var gdr = value as GenericDbRecord<T> ?? new GenericDbRecord<T>(value);
    //        return DoConvert(gdr, fieldNameFormat);
    //    }
    //
    //    protected sealed override T DoConvert(IDbRecord source)
    //    {
    //        var gdr = source as GenericDbRecord<T> ?? new GenericDbRecord<T>(source);
    //        return DoConvert(gdr, "{0}");
    //    }
    //    protected abstract T DoConvert(GenericDbRecord<T> record, string fieldNameFormat);
    //}
}