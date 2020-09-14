using System;
using System.Globalization;
using System.IO;
using Axle.Verification;

namespace Axle.Resources
{
    /// <summary>
    /// A representation of a binary resource.
    /// </summary>
    public sealed class BinaryResourceInfo : ResourceInfo
    {
        private readonly byte[] _value;

        /// <summary>
        /// Creates a new instance of the <seealso cref="BinaryResourceInfo"/> class.
        /// </summary>
        public BinaryResourceInfo(string name, CultureInfo culture, byte[] value) : base(name, culture, "application/octet-stream")
        {
            _value = value.VerifyArgument(nameof(value)).IsNotNull();
        }

        /// <inheritdoc />
        public override Stream Open()
        {
            return new MemoryStream(_value);
        }

        /// <inheritdoc />
        public override bool TryResolve(Type type, out object result)
        {
            if (type == typeof(byte[]))
            {
                result = _value;
                return true;
            }
            if (type == typeof(BinaryReader))
            {
                result = new BinaryReader(Open());
                return true;
            }
            return base.TryResolve(type, out result);
        }

        /// <summary>
        /// Gets the binary data that the current <see cref="BinaryResourceInfo"/> instance represents.
        /// </summary>
        public byte[] Value => _value;
    }
}