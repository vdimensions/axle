using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Axle.Verification;

namespace Axle.Text.Documents.Binding
{
    internal sealed class DocumentDictionaryValueProvider : IDocumentComplexValueProvider
    {
        private static IEnumerable<IDocumentValueProvider> ExpandChildren(IDocumentValueProvider documentValueProvider)
        {
            switch (documentValueProvider)
            {
                case IDocumentComplexValueProvider complexValueProvider:
                    foreach (var child in complexValueProvider.GetChildren())
                    {
                        yield return child;
                    }
                    break;
                case IDocumentCollectionValueProvider collectionValueProvider:
                    foreach (var sibling in collectionValueProvider)
                    {
                        foreach (var child in ExpandChildren(sibling))
                        {
                            yield return child;
                        }
                    }
                    break;
            }
        }
        
        private readonly Dictionary<string, IDocumentValueProvider> _valueValueProviders;

        public DocumentDictionaryValueProvider(IDocumentCollectionValueProvider valueProvider)
        {
            Name = valueProvider.Name;
            var comparer = StringComparer.Ordinal;
            _valueValueProviders = valueProvider
                .SelectMany(ExpandChildren)
                .GroupBy(x => x.Name, comparer)
                .ToDictionary(
                    x => x.Key,
                    x => (IDocumentValueProvider) new DocumentCollectionValueProvider(x.Key, x.ToArray()),
                    comparer);
        }

        public bool TryGetValue(string member, out IDocumentValueProvider value) => _valueValueProviders.TryGetValue(member, out value);

        public IEnumerable<IDocumentValueProvider> GetChildren() => _valueValueProviders.Values;

        public string Name { get; }

        public IDocumentValueProvider this[string member] => TryGetValue(member, out var value) ? value : null;
    }
    
    /// <summary>
    /// A general-purpose implementation of the <see cref="IDocumentBinder"/> interface.
    /// </summary>
    /// <seealso cref="IDocumentBinder"/>
    public class DefaultDocumentBinder : IDocumentBinder
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultDocumentBinder"/> class using the specified 
        /// <paramref name="objectProvider"/> and <paramref name="converter"/>.
        /// </summary>
        /// <param name="objectProvider">
        /// A <see cref="IObjectProvider"/> instance that enables creation and member access 
        /// to the object instance being bound.
        /// </param>
        /// <param name="converter">
        /// A <see cref="IBindingConverter"/> instance that handles the conversion of the raw data values
        /// during the binding process.
        /// </param>
        public DefaultDocumentBinder(IObjectProvider objectProvider, IBindingConverter converter)
        {
            ObjectProvider = Verifier.IsNotNull(Verifier.VerifyArgument(objectProvider, nameof(objectProvider))).Value;
            Converter = Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter))).Value;
        }
        #if NETSTANDARD1_5_OR_NEWER || NET20_OR_NEWER
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultDocumentBinder"/> class using a 
        /// <see cref="ReflectionObjectProvider"/> and a <see cref="DefaultBindingConverter"/>
        /// </summary>
        public DefaultDocumentBinder() : this(new ReflectionObjectProvider(), new DefaultBindingConverter()) { }
        #endif

        private static bool TryBind(
                IObjectProvider objectProvider, 
                IBindingConverter converter, 
                IDocumentValueProvider valueProvider, 
                bool isDictionary,
                object instance, 
                Type targetType, 
                out object boundValue)
        {
            switch (valueProvider)
            {
                case IDocumentSimpleValueProvider svp:
                    return converter.TryConvertMemberValue(svp.Value, targetType, out boundValue);
                
                case IDocumentCollectionValueProvider collectionProvider:
                    var collectionAdapter = collectionProvider.ValueAdapter ??
                                            objectProvider.GetCollectionAdapter(targetType);
                    switch (collectionAdapter)
                    {
                        case null:
                            return TryBind(
                                objectProvider,
                                converter,
                                collectionProvider.LastOrDefault(),
                                false,
                                instance,
                                targetType,
                                out boundValue);
                        case IDocumentDictionaryValueAdapter dva:
                            var dictionaryValueProviders = new DocumentDictionaryValueProvider(collectionProvider)
                                .GetChildren()
                                .ToArray();
                            var dictionaryValues = new Dictionary<string, object>(StringComparer.Ordinal);
                            for (var i = 0; i < dictionaryValueProviders.Length; ++i)
                            {
                                var provider = dictionaryValueProviders[i];
                                var value = TryBind(
                                    objectProvider,
                                    converter,
                                    provider,
                                    false,
                                    instance != null ? dva.ItemAt((IDictionary) instance, provider.Name) : null,
                                    collectionAdapter.ElementType,
                                    out var item)
                                    ? item
                                    : null;
                                dictionaryValues[provider.Name] = value;
                            }

                            boundValue = dva.SetItems(instance, dictionaryValues);
                            return true;
                        default:
                            var collectionValueProviders = collectionProvider.ToArray();
                            var collectionValues = new object[collectionValueProviders.Length];
                            for (var i = 0; i < collectionValueProviders.Length; ++i)
                            {
                                var provider = collectionValueProviders[i];
                                var value = TryBind(
                                    objectProvider,
                                    converter,
                                    provider,
                                    false,
                                    instance != null ? collectionAdapter.ItemAt((IEnumerable) instance, i) : null,
                                    collectionAdapter.ElementType,
                                    out var item)
                                    ? item
                                    : null;
                                collectionValues[i] = value;
                            }

                            boundValue = collectionAdapter.SetItems(instance, collectionValues);
                            return true;
                    }
                case IDocumentComplexValueProvider complexProvider:
                    var cAdapter = objectProvider.GetCollectionAdapter(targetType);
                    if (instance == null)
                    {
                        instance = objectProvider.CreateInstance(targetType);
                    }
                    if (cAdapter != null)
                    {
                        return TryBind(
                            objectProvider,
                            converter,
                            new DocumentCollectionValueProvider(complexProvider.Name, cAdapter, complexProvider),
                            false,
                            instance,
                            targetType,
                            out boundValue);
                    }

                    var members = objectProvider.GetMembers(instance);
                    foreach (var member in members)
                    {
                        if (complexProvider.TryGetValue(member.Name, out var memberValueProvider))
                        {
                            var memberType = member.MemberType;
                            var memberInstance = member.GetAccessor?.GetValue(instance);

                            if (TryBind(
                                objectProvider,
                                converter,
                                memberValueProvider,
                                false,
                                memberInstance,
                                memberType,
                                out var memberValue))
                            {
                                member.SetAccessor.SetValue(instance, memberValue);
                            }
                        }
                    }

                    boundValue = instance;
                    return true;
            }
            boundValue = null;
            return false;
        }

        /// <inheritdoc />
        public object Bind(IDocumentValueProvider memberValueProvider, object instance)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(memberValueProvider, nameof(memberValueProvider)));
            Verifier.IsNotNull(Verifier.VerifyArgument(instance, nameof(instance)));
            return TryBind(
                    ObjectProvider, 
                    Converter, 
                    memberValueProvider, 
                    false,
                    instance, 
                    instance.GetType(), 
                    out var boundValue) 
                ? boundValue 
                : instance;
        }

        /// <inheritdoc />
        public object Bind(IDocumentValueProvider memberValueProvider, Type type)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(memberValueProvider, nameof(memberValueProvider)));
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            var instance = ObjectProvider.CreateInstance(type);
            return TryBind(ObjectProvider, Converter, memberValueProvider, false, instance, type, out var boundValue) 
                ? boundValue 
                : instance;
        }

        /// <summary>
        /// Gets the current <see cref="IObjectProvider"/> instance that enables creation and member access 
        /// to the object instance being bound.
        /// </summary>
        public IObjectProvider ObjectProvider { get; }

        /// <summary>
        /// Gets the current <see cref="IBindingConverter"/> instance that handles the conversion of the raw data 
        /// values during the binding process.
        /// </summary>
        public IBindingConverter Converter { get; }
    }
}
