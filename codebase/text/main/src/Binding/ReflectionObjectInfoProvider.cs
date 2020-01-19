#if NETSTANDARD1_5_OR_NEWER || NET35_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Axle.Reflection;
using Axle.Reflection.Extensions.Type;

namespace Axle.Text.StructuredData.Binding
{
    /// <summary>
    /// An implementation of the <see cref="IBindingObjectInfoProvider"/> that uses reflection.
    /// </summary>
    public sealed class ReflectionObjectInfoProvider : IBindingObjectInfoProvider
    {
        internal abstract class CollectionAdapter : IBindingCollectionAdapter
        {
            protected CollectionAdapter(Type collectionType, Type elementType)
            {
                CollectionType = collectionType;
                ElementType = elementType;
            }

            public virtual object ItemAt(IEnumerable collection, int index) 
                => collection.OfType<object>().Skip(index).Take(1);

            public abstract object SetItems(object collection, IEnumerable items);

            public Type CollectionType { get; }
            public Type ElementType { get; }
        }
        
        internal sealed class GenericListAdapter : CollectionAdapter
        {
            private static Type GetElementType(Type genericType)
            {
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                return genericType.GetGenericArguments()[0];
                #else
                return genericType.GetTypeInfo().GetGenericArguments()[0];
                #endif
            }
            public GenericListAdapter(Type collectionType) : base(collectionType, GetElementType(collectionType)) { }

            public override object SetItems(object collection, IEnumerable items)
            {
                throw new NotImplementedException();
            }
        }
        internal sealed class GenericArrayAdapter : CollectionAdapter
        {
            public GenericArrayAdapter(Type elementType) : base(elementType.MakeArrayType(1), elementType) { }

            public override object SetItems(object collection, IEnumerable items)
            {
                throw new NotImplementedException();
            }
        }
        
        /// <inheritdoc/>
        public IReadWriteMember GetMember(object instance, string member)
        {
            return GetMembers(instance).SingleOrDefault(x => StringComparer.Ordinal.Equals(member, x.Name));
        }

        /// <inheritdoc/>
        public IReadWriteMember[] GetMembers(object instance)
        {
            switch (instance)
            {
                case null:
                case bool _:
                case sbyte _:
                case byte _:
                case short _:
                case ushort _:
                case int _:
                case uint _:
                case long _:
                case ulong _:
                case float _:
                case double _:
                case decimal _:
                case char _:
                case string _:
                case DateTime _:
                case DateTimeOffset _:
                case TimeSpan _:
                case Guid _:
                #if NETSTANDARD2_0_OR_NEWER || NET35_OR_NEWER
                case DBNull _:
                #endif
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                case object obj when obj.GetType().IsEnum || obj.GetType().IsDelegate() || obj.GetType().IsNullableType():
                #else
                case object obj when obj.GetType().IsDelegate() || obj.GetType().IsNullableType():
                #endif
                    return new IReadWriteMember[0]; 
            }
            return new TypeIntrospector(instance.GetType())
                .GetMembers(ScanOptions.Instance | ScanOptions.Public)
                .Select(x => x as IReadWriteMember)
                .Where(x => x != null)
                .ToArray();
        }
        
        /// <inheritdoc/>
        public object CreateInstance(Type type)
        {
            var remappedIntrospector = RemapType(type);
            try
            {
                return remappedIntrospector.GetConstructor(ScanOptions.Instance | ScanOptions.Public)?.Invoke();
            }
            catch
            {
                return null;
            }
        }

        private TypeIntrospector RemapType(Type type)
        {
            var introspector = new TypeIntrospector(type);
            //if (introspector.IsGenericType)
            //{
                //var genericType
            //}
            return introspector;
        }

        public IBindingCollectionAdapter GetCollectionAdapter(Type type)
        {
            var introspector = new TypeIntrospector(type);
            if (introspector.IsGenericType)
            {
                var definitionType = introspector.GetGenericTypeDefinition().GenericDefinitionType;
                if (definitionType == typeof(List<>))
                {
                    return new GenericListAdapter(type);
                }
                else if (definitionType.IsArray)
                {
                    return new GenericListAdapter(type);
                }
            }

            return null;
        }
    }
}
#endif