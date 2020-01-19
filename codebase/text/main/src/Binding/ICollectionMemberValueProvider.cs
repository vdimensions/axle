using System.Collections.Generic;

namespace Axle.Text.StructuredData.Binding
{
    public interface ICollectionMemberValueProvider : IBindingValueProvider, IEnumerable<IBindingValueProvider>
    {
        IBindingCollectionAdapter CollectionAdapter { get; }
    }
}
