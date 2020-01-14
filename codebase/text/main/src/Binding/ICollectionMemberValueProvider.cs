using System.Collections.Generic;

namespace Axle.Text.StructuredData.Binding
{
    public interface ICollectionMemberValueProvider : IBindingValueProvider
    {
        bool TryGetValues(out IEnumerable<IBindingValueProvider> values);
    }
}
