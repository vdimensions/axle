﻿using System.Diagnostics.CodeAnalysis;

namespace Axle.Conversion
{
    /// <summary>
    /// A class that can be used to convert values to and from <see cref="byte"/> and <see cref="double"/>.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class ByteToDoubleConverter : AbstractTwoWayConverter<byte, double>
    {
        /// <summary>
        /// Gets a reference to a shared <see cref="ByteToDoubleConverter"/> instance.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")] 
        public static readonly ByteToDoubleConverter Instance = new ByteToDoubleConverter();
        
        /// <inheritdoc />
        protected override double DoConvert(byte source) => source;

        /// <inheritdoc />
        protected override byte DoConvertBack(double source) => (byte) source;
    }
}
