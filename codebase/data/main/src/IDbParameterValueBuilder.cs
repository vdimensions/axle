using System;
using System.Data;

namespace Axle.Data
{
    public interface IDbParameterValueBuilder : IDbParameterTypeBuilder
    {
        [Obsolete]
        IDbParameterOptionalPropertiesBuilder SetValue(DbType type, object value);
        IDbParameterOptionalPropertiesBuilder SetValue(object value);
    }
}