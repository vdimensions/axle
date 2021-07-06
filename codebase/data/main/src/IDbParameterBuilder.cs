using System.Data;

namespace Axle.Data
{
    /// <summary>
    /// An interface representing a builder for database query parameters.
    /// </summary>
    public interface IDbParameterBuilder
    {
        /// <summary>
        /// Initializes a new <see cref="IDbParameterValueBuilder"/> instance using the provided
        /// parameter <paramref name="name"/> and parameter <paramref name="direction"/>.
        /// </summary>
        /// <param name="name">
        /// The name for the db parameter to build.
        /// </param>
        /// <param name="direction">
        /// One of the possible values of the <see cref="ParameterDirection"/> enumeration, describing
        /// the desired parameter direction.
        /// </param>
        /// <returns>
        /// A <see cref="IDbParameterValueBuilder"/> instance that allows to further specify
        /// the parameter's value, data type and length constraints.
        /// </returns>
        IDbParameterValueBuilder CreateParameter(string name, ParameterDirection direction);
    }
}