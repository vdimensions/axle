﻿namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="uint"/> and <see cref="decimal"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class UInt32ToDecimalConverter : AbstractTwoWayConverter<uint, decimal>
    {
        /// <inheritdoc />
        protected override decimal DoConvert(uint source) => source;

        /// <inheritdoc />
        protected override uint DoConvertBack(decimal source) => (uint) source;
    }
}
