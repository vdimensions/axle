using System;

namespace Axle.Conversion.Binding
{
    public interface IBindingConverter
    {
        bool TryConvertMemberValue(string rawValue, Type targetType, out object boundValue);
    }
}
