using System.Data;

using Axle.Conversion;


namespace Axle.Data.Conversion
{
    public interface IDbValueConverter : ITwoWayConverter<object, object>
    {
        DbType DbType { get; }
    }
}