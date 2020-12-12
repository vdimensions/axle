namespace Axle.Data
{
    /// <summary>
    /// An interface that enables setting values to a <see cref="System.Data.IDataParameter"/> instance which is being
    /// created by a <see cref="IDbParameterBuilder"/>.
    /// </summary>
    public interface IDbParameterValueBuilder
    {
        /// <summary>
        /// Sets the provided <paramref name="value"/> as the value for the <see cref="System.Data.IDataParameter"/>
        /// instance that will be built by the current <see cref="IDbParameterValueBuilder"/>.
        /// </summary>
        /// <param name="value">
        /// The value to be applied to the parameter.
        /// </param>
        /// <returns>
        /// An instance of <see cref="IDbParameterOptionalPropertiesBuilder"/> instance which allows configuring
        /// optional properties (such as lenght and db type) for the <see cref="System.Data.IDataParameter"/> that is
        /// being built.
        /// </returns>
        /// <seealso cref="IDbParameterOptionalPropertiesBuilder"/>
        IDbParameterOptionalPropertiesBuilder SetValue(object value);
    }
}