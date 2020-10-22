using System;

namespace Axle.Data.Records.Mapping.Accessors
{
    internal interface IFieldTypeOverride
    {
        Type OverridenFieldType { get; }
    }
}