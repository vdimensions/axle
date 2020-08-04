using System.Data;

namespace Axle.Data
{
    /// <summary>
    /// An interface representing a builder for database query parameters.
    /// </summary>
    public interface IDbParameterBuilder
    {
        IDbParameterValueBuilder CreateParameter(string name, ParameterDirection direction);
    }
}