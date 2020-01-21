using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Axle.Text.StructuredData.Binding
{
    public sealed class TextDataProvider : IComplexMemberValueProvider
    {
        private static IBindingValueProvider GetProvider(IStructuredDataNode node)
        {
            switch (node)
            {
                case IStructuredDataValue v:
                    return new SVP(v);
                case IStructuredDataObject o:
                    return new SOP(o);
            }
            throw new NotSupportedException($"Unsupported node type {node.GetType().FullName}");
        }

        private sealed class SOP : IComplexMemberValueProvider
        {
            private readonly IStructuredDataObject _provider;

            public SOP(IStructuredDataObject provider)
            {
                _provider = provider;
            }

            public bool TryGetValue(string member, out IBindingValueProvider value)
            {
                value = new CVP(member, _provider.GetChildren(member).Select(GetProvider));
                return true;
            }

            public string Name => _provider.Key;

            public IBindingValueProvider this[string member] => TryGetValue(member, out var value) ? value : null;
        }

        private sealed class SVP : ISimpleMemberValueProvider
        {
            private readonly IStructuredDataValue _provider;

            public SVP(IStructuredDataValue provider)
            {
                _provider = provider;
            }

            public string Name => _provider.Key;
            public string Value => _provider.Value;
        }

        private sealed class CVP : ICollectionMemberValueProvider
        {
            private readonly IEnumerable<IBindingValueProvider> _providers;

            public CVP(string name, IEnumerable<IBindingValueProvider> providers)
            {
                _providers = providers;
                Name = name;
            }

            public IEnumerator<IBindingValueProvider> GetEnumerator() => _providers.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public string Name { get; }

            IBindingCollectionAdapter ICollectionMemberValueProvider.CollectionAdapter => null;
        }

        private readonly SOP _inner;

        public TextDataProvider(IStructuredDataRoot structuredData) { _inner = (SOP) GetProvider(structuredData); }

        public bool TryGetValue(string member, out IBindingValueProvider value) => _inner.TryGetValue(member, out value);

        public string Name => "";

        public IBindingValueProvider this[string member] => _inner[member];
    }
}
