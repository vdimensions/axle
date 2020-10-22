#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;

namespace Axle.Data.Records.Mapping.Accessors.DataFields
{
    public sealed class DateTimeOffsetDataFieldAccessor : AbstractDataFieldAccessor<DateTimeOffset> { }
}
#endif