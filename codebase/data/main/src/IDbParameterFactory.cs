using System.Data;
using System.Data.Common;

namespace Axle.Data
{
    /// <summary>
    /// An interface representing a <see cref="DbCommand">command</see> <see cref="DbParameter">parameter</see>
    /// factory.
    /// </summary>
    /// <typeparam name="TDbParameter">
    /// The concrete <see cref="DbParameter"/> type this factory supports.
    /// </typeparam>
    /// <typeparam name="TDbType">
    /// The database-specific type used in the parameter definition.
    /// </typeparam>
    /// <seealso cref="IDbDataParameter"/>
    /// <seealso cref="IDataParameter"/>
    /// <seealso cref="DbParameter"/>
    public interface IDbParameterFactory<out TDbParameter, TDbType>
        where TDbParameter: DbParameter
        where TDbType: struct
    {
        /// <summary>
        /// Creates a new instance of <typeparamref name="TDbParameter"/> using the provided value and definition
        /// settings.
        /// </summary>
        /// <param name="name">
        /// The name of the parameter.
        /// </param>
        /// <param name="type">
        /// An optional parameter of <typeparamref name="TDbType"/> type, specifying the database-specific type of the
        /// parameter.
        /// </param>
        /// <param name="size">
        /// An optional <see cref="int">integer</see> value specifying the size of the parameter.
        /// </param>
        /// <param name="direction">
        /// One of the <see cref="ParameterDirection"/> enumeration values.
        /// </param>
        /// <param name="value">
        /// The actual value passed to the db parameter.
        /// </param>
        /// <returns>
        /// A new instance of <typeparamref name="TDbParameter"/> using the provided value and definition settings.
        /// </returns>
        TDbParameter CreateDbParameter(string name, TDbType? type, int? size, ParameterDirection direction, object value);
    }
}