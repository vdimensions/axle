using System.Data;
using System.Diagnostics;

using Axle.Conversion;
using Axle.Data.Conversion;


namespace Axle.Data.Common.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    public abstract class DbTypeConverter<T1, T2> : AbstractTwoWayConverter<T1, T2>, IDbValueConverter
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly DbType _dbType;

        protected DbTypeConverter(DbType dbType)
        {
            _dbType = dbType;
        }

        protected abstract T2 GetNotNullValue(T1 value);
        protected abstract T1 GetNotNullValue(T2 value);

        protected virtual bool IsStub(T1 value) => Equals(value, SourceNullEquivalent);
        protected virtual bool IsNull(T2 value) => Equals(value, DestinationNullEquivalent);

        protected sealed override T2 DoConvert(T1 source) => !IsStub(source) ? GetNotNullValue(source) : DestinationNullEquivalent;
        protected sealed override T1 DoConvertBack(T2 source) => IsNull(source) ? SourceNullEquivalent : GetNotNullValue(source);

        protected abstract T1 SourceNullEquivalent { get; }
        protected abstract T2 DestinationNullEquivalent { get; }

        public object Convert(object source) => DoConvert((T1) source);

        public object ConvertBack(object source) => DoConvertBack((T2) source);

        IConverter<object, object> ITwoWayConverter<object, object>.Invert() => new ReverseConverter<object, object>(this);

        public bool TryConvert(object source, out object target)
        {
            if (TryConvert((T1) source, out T2 res))
            {
                target = res;
                return true;
            }
            target = null;
            return false;
        }

        public bool TryConvertBack(object source, out object target)
        {
            if (TryConvertBack((T2) source, out T1 res))
            {
                target = res;
                return true;
            }
            target = null;
            return false;
        }

        public DbType DbType => _dbType;
    }
}