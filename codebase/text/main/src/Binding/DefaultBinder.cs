using Axle.Verification;
using System;
using System.Linq;

namespace Axle.Text.StructuredData.Binding
{
    /// <summary>
    /// A general-purpose implementation of the <see cref="IBinder"/> interface.
    /// </summary>
    /// <seealso cref="IBinder"/>
    public class DefaultBinder : IBinder
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultBinder"/> class using the specified 
        /// <paramref name="objectInfoProvider"/> and <paramref name="converter"/>.
        /// </summary>
        /// <param name="objectInfoProvider">
        /// A <see cref="IBindingObjectInfoProvider"/> instance that enables creation and member access 
        /// to the object instance being bound.
        /// </param>
        /// <param name="converter">
        /// A <see cref="IBindingConverter"/> instance that handles the conversion of the raw data values
        /// during the binding process.
        /// </param>
        public DefaultBinder(IBindingObjectInfoProvider objectInfoProvider, IBindingConverter converter)
        {
            ObjectInfoProvider = Verifier.IsNotNull(Verifier.VerifyArgument(objectInfoProvider, nameof(objectInfoProvider))).Value;
            Converter = Verifier.IsNotNull(Verifier.VerifyArgument(converter, nameof(converter))).Value;
        }
        #if NETSTANDARD1_5_OR_NEWER || NET35_OR_NEWER
        /// <summary>
        /// Creates a new instance of the <see cref="DefaultBinder"/> class using a 
        /// <see cref="ReflectionObjectInfoProvider"/> and a <see cref="DefaultBindingConverter"/>
        /// </summary>
        public DefaultBinder() : this(new ReflectionObjectInfoProvider(), new DefaultBindingConverter()) { }
        #endif

        private static bool IsCollection(object instance, out Type elementType)
        {
            // TODO: determine if the passed in object is a collection
            elementType = null;
            return false;
        }

        private static object GetCollectionItem(object collection, int index)
        {
            throw new NotImplementedException();
        }

        private static bool TryBind(
                IBindingObjectInfoProvider objectInfoProvider, 
                IBindingConverter converter, 
                IBindingValueProvider valueProvider, 
                object instance, 
                Type targetType, 
                out object boundValue)
        {
            switch (valueProvider)
            {
                case ISimpleMemberValueProvider svp:
                    return converter.TryConvertMemberValue(svp.Value, targetType, out boundValue);
                case ICollectionMemberValueProvider collectionProvider:
                    if (instance == null)
                    {
                        break;
                    }
                    // TODO: make method not a try attempt
                    if (collectionProvider.TryGetValues(out var providers))
                    {
                        if (IsCollection(instance, out var elementType))
                        {
                            var items = providers.Select(
                                (provider, i) =>
                                {
                                    if (TryBind(
                                        objectInfoProvider,
                                        converter,
                                        provider,
                                        GetCollectionItem(instance, i),
                                        elementType,
                                        out var item))
                                    {
                                        return instance;
                                    }
                                    return null;
                                });
                            //TODO: set items to instance somehow
                            boundValue = instance;
                            return true;
                        }
                        else
                        {
                            return TryBind(
                                objectInfoProvider, 
                                converter, 
                                providers.Last(), 
                                instance, 
                                targetType, 
                                out boundValue);
                        }                        
                    }

                    break;
                case IComplexMemberValueProvider complexProvider:
                    if (instance == null)
                    {
                        break;
                    }
                    if (IsCollection(instance, out var _))
                    {
                        //TODO: convert this provider to singleton collection provider instead
                        // and invoke TryBind again.
                    }
                    var members = objectInfoProvider.GetMembers(instance);
                    foreach (var member in members)
                    {
                        // TODO: if member is collection. call cvp.TryGetValues(member.Name, out var valueProviders)

                        if (complexProvider.TryGetValue(member.Name, out var memberValueProvider))
                        {
                            var memberType = member.MemberType;
                            var memberInstance =
                                member.GetAccessor?.GetValue(instance) ?? objectInfoProvider.CreateInstance(memberType);

                            if (TryBind(
                                objectInfoProvider,
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
        public object Bind(IBindingValueProvider memberValueProvider, object instance)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(memberValueProvider, nameof(memberValueProvider)));
            Verifier.IsNotNull(Verifier.VerifyArgument(instance, nameof(instance)));
            return TryBind(
                    ObjectInfoProvider, 
                    Converter, 
                    memberValueProvider, 
                    instance, 
                    instance.GetType(), 
                    out var boundValue) 
                ? boundValue 
                : instance;
        }

        /// <inheritdoc />
        public object Bind(IBindingValueProvider memberValueProvider, Type type)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(memberValueProvider, nameof(memberValueProvider)));
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            var instance = ObjectInfoProvider.CreateInstance(type);
            return TryBind(ObjectInfoProvider, Converter, memberValueProvider, instance, type, out var boundValue) 
                ? boundValue 
                : instance;
        }

        /// <summary>
        /// Gets the current <see cref="IBindingObjectInfoProvider"/> instance that enables creation and member access 
        /// to the object instance being bound.
        /// </summary>
        public IBindingObjectInfoProvider ObjectInfoProvider { get; }

        /// <summary>
        /// Gets the current <see cref="IBindingConverter"/> instance that handles the conversion of the raw data 
        /// values during the binding process.
        /// </summary>
        public IBindingConverter Converter { get; }
    }
}
