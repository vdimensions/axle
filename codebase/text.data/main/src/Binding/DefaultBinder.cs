using Axle.Verification;
using System;
using System.Collections;
using System.Linq;

namespace Axle.Text.Data.Binding
{
    /// <summary>
    /// A general-purpose implementation of the <see cref="IBinder"/> interface.
    /// </summary>
    /// <seealso cref="IBinder"/>
    public class DefaultBinder : IBinder
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultBinder"/> class using the specified 
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
        public DefaultBinder(IObjectProvider objectProvider, IBindingConverter converter)
        {
            ObjectProvider = Verifier.IsNotNull(Verifier.VerifyArgument(objectProvider, nameof(objectProvider))).Value;
            Converter = Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter))).Value;
        }
        #if NETSTANDARD1_5_OR_NEWER || NET35_OR_NEWER
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultBinder"/> class using a 
        /// <see cref="ReflectionObjectProvider"/> and a <see cref="DefaultBindingConverter"/>
        /// </summary>
        public DefaultBinder() : this(new ReflectionObjectProvider(), new DefaultBindingConverter()) { }
        #endif

        private static bool TryBind(
                IObjectProvider objectProvider, 
                IBindingConverter converter, 
                IBoundValueProvider valueProvider, 
                object instance, 
                Type targetType, 
                out object boundValue)
        {
            switch (valueProvider)
            {
                case IBoundSimpleValueProvider svp:
                    return converter.TryConvertMemberValue(svp.Value, targetType, out boundValue);
                
                case IBoundCollectionProvider collectionProvider:
                    var collectionAdapter = collectionProvider.CollectionAdapter ??
                                            objectProvider.GetCollectionAdapter(targetType);
                    if (collectionAdapter != null)
                    {
                        var items = collectionProvider.Select(
                            (provider, i) => TryBind(
                                objectProvider,
                                converter,
                                provider,
                                instance != null ? collectionAdapter.ItemAt((IEnumerable) instance, i) : null,
                                collectionAdapter.ElementType,
                                out var item) ? item : null);
                        boundValue = collectionAdapter.SetItems(instance, items);
                        return true;
                    }
                    return TryBind(
                        objectProvider, 
                        converter, 
                        collectionProvider.Last(), 
                        instance, 
                        targetType, 
                        out boundValue);

                case IBoundComplexValueProvider complexProvider:
                    var cAdapter = objectProvider.GetCollectionAdapter(targetType);
                    if (cAdapter != null)
                    {
                        return TryBind(
                            objectProvider,
                            converter,
                            new BoundCollectionProvider(complexProvider.Name, cAdapter, complexProvider),
                            instance,
                            targetType,
                            out boundValue);
                    }
                    if (instance == null)
                    {
                        instance = objectProvider.CreateInstance(targetType);
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
        public object Bind(IBoundValueProvider memberValueProvider, object instance)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(memberValueProvider, nameof(memberValueProvider)));
            Verifier.IsNotNull(Verifier.VerifyArgument(instance, nameof(instance)));
            return TryBind(
                    ObjectProvider, 
                    Converter, 
                    memberValueProvider, 
                    instance, 
                    instance.GetType(), 
                    out var boundValue) 
                ? boundValue 
                : instance;
        }

        /// <inheritdoc />
        public object Bind(IBoundValueProvider memberValueProvider, Type type)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(memberValueProvider, nameof(memberValueProvider)));
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            var instance = ObjectProvider.CreateInstance(type);
            return TryBind(ObjectProvider, Converter, memberValueProvider, instance, type, out var boundValue) 
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
