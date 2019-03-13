using System.Data;

using Axle.Conversion;


namespace Axle.Data.Conversion
{
    /// <summary>
    /// An interface used for converting database-specific value objects to their raw counterparts and
    /// the other way around.
    /// </summary>
    public interface IDbValueConverter : ITwoWayConverter<object, object>
    {
        DbType DbType { get; }
    }
}