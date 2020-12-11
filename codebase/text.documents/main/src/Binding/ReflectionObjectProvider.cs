#if NETSTANDARD1_5_OR_NEWER || NET20_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Axle.Reflection;

namespace Axle.Text.Documents.Binding
{
    /// <summary>
    /// An implementation of the <see cref="IObjectProvider"/> that uses reflection.
    /// </summary>
    public sealed class ReflectionObjectProvider : IObjectProvider
    {
        internal abstract class CollectionValueAdapter : IDocumentCollectionValueAdapter
        {
            protected CollectionValueAdapter(Type elementType)
            {
                ElementType = elementType;
            }

            public virtual object ItemAt(IEnumerable collection, int index) 
                => collection.OfType<object>().Skip(index).Take(1);

            public abstract object SetItems(object collection, IEnumerable items);

            public Type ElementType { get; }
        }

        internal abstract class RawCollectionValueAdapter<TCollection> : CollectionValueAdapter
            where TCollection : class, IEnumerable
        {
            protected RawCollectionValueAdapter() : base(typeof(object)) { }

            public sealed override object ItemAt(IEnumerable collection, int index)
            {
                return ItemAt((TCollection) collection, index);
            }
            public virtual object ItemAt(TCollection collection, int index) => collection.OfType<object>().Skip(index).Take(1);

            public sealed override object SetItems(object collection, IEnumerable items)
            {
                return SetItems(collection as TCollection, items);
            }
            public abstract TCollection SetItems(TCollection collection, IEnumerable items);
        }

        internal abstract class GenericCollectionValueAdapter<T, TCollection> : CollectionValueAdapter
            where TCollection: class, IEnumerable<T>
        {
            protected GenericCollectionValueAdapter() : base(typeof(T)) { }

            public sealed override object ItemAt(IEnumerable collection, int index)
            {
                return ItemAt((TCollection) collection, index);
            }
            public virtual object ItemAt(TCollection collection, int index) => collection.Skip(index).Take(1);

            public sealed override object SetItems(object collection, IEnumerable items)
            {
                return SetItems(collection as TCollection, (items as IEnumerable<T>) ?? items.Cast<T>());
            }
            public abstract TCollection SetItems(TCollection collection, IEnumerable<T> items);
        }

        internal abstract class AbstractGenericListValueAdapter<T, TList> : GenericCollectionValueAdapter<T, TList> where TList: class, IList<T>
        {
            public sealed override object ItemAt(TList collection, int index)
            {
                if (collection.Count > index)
                {
                    return collection[index];
                }
                else
                {
                    return null;
                }
            }
        }

        internal sealed class GenericArrayValueAdapter<T> : AbstractGenericListValueAdapter<T, T[]>
        {
            public override T[] SetItems(T[] collection, IEnumerable<T> items)
            {
                var data = items.ToArray();
                if (collection == null || data.Length != collection.Length)
                {
                    return data;
                }
                
                for (var i = 0; i < data.Length; i++)
                {
                    collection[i] = data[i];
                }
                return collection;
            }
        }

        internal sealed class GenericListValueAdapter<T> : AbstractGenericListValueAdapter<T, List<T>>
        {
            public override List<T> SetItems(List<T> collection, IEnumerable<T> items)
            {
                if (collection == null)
                {
                    return items.ToList();
                }
                collection.Clear();
                collection.AddRange(items);
                return collection;
            }
        }

        internal sealed class GenericLinkedListValueAdapter<T> : GenericCollectionValueAdapter<T, LinkedList<T>>
        {
            public override LinkedList<T> SetItems(LinkedList<T> collection, IEnumerable<T> items)
            {
                if (collection != null)
                {
                    collection.Clear();
                    foreach (var item in items)
                    {
                        collection.AddLast(item);
                    }
                } 
                else
                {
                    collection = new LinkedList<T>(items);
                }
                return collection;
            }
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        internal sealed class RawListValueAdapter : RawCollectionValueAdapter<ArrayList>
        {
            public sealed override object ItemAt(ArrayList collection, int index)
            {
                if (collection.Count > index)
                {
                    return collection[index];
                }
                else
                {
                    return null;
                }
            }

            public override ArrayList SetItems(ArrayList collection, IEnumerable items)
            {
                if (collection == null)
                {
                    collection = new ArrayList();
                }
                else
                {
                    collection.Clear();
                }
                foreach (var item in items)
                {
                    collection.Add(item);
                }
                return collection;
            }
        }
        #endif
        
        internal sealed class GenericDictionaryAdapter<TKey, T> : IDocumentDictionaryValueAdapter
        {
            object IDocumentCollectionValueAdapter.ItemAt(IEnumerable collection, int index) => throw new NotSupportedException();
            public object ItemAt(IDictionary dictionary, object key) => dictionary.Contains(key) ? dictionary[key] : null;

            object IDocumentCollectionValueAdapter.SetItems(object collection, IEnumerable items) => SetItems(collection as IDictionary, items as IDictionary);
            public object SetItems(IDictionary dictionary, IDictionary items)
            {
                if (dictionary == null)
                {
                    dictionary = new Dictionary<TKey, T>();
                }
                else
                {
                    dictionary.Clear();
                }
                foreach (DictionaryEntry entry in items)
                {
                    dictionary.Add(entry.Key, entry.Value);
                }
                return dictionary;
            }

            public Type KeyType => typeof(TKey);
            public Type ElementType => typeof(T);
        }

        private static object CreateInstance(ITypeIntrospector introspector)
        {
            try
            {
                return introspector.CreateInstance();
            }
            catch
            {
                return null;
            }
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
                    return new IReadWriteMember[0]; 
                case object obj:
                    var introspector = new TypeIntrospector(obj.GetType());
                    var flags = introspector.TypeFlags;
                    var unusableObjectFlags = new[]
                    {
                        TypeFlags.Primitive,
                        TypeFlags.ValueType,
                        TypeFlags.Delegate,
                        TypeFlags.Enum,
                        TypeFlags.NullableValueType,
                        TypeFlags.Attribute
                    };
                    if (flags.HasAnyFlag(unusableObjectFlags))
                    {
                        return new IReadWriteMember[0]; 
                    }
                    break;
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
            return CreateInstance(remappedIntrospector);
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

        public IDocumentCollectionValueAdapter GetCollectionAdapter(Type type)
        {
            var introspector = new TypeIntrospector(type);
            Type collectionType = type, adapterType = null;
            var elementTypes = new Type[0];
            var adapterTypesMap = new Dictionary<Type, Type>()
            {
                {typeof(LinkedList<>), typeof(GenericLinkedListValueAdapter<>)},
                {typeof(List<>), typeof(GenericListValueAdapter<>)},
                {typeof(IList<>), typeof(GenericListValueAdapter<>)},
                {typeof(ICollection<>), typeof(GenericListValueAdapter<>)},
                {typeof(IEnumerable<>), typeof(GenericListValueAdapter<>)},
                {typeof(IDictionary<,>), typeof(GenericDictionaryAdapter<,>)},
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                {typeof(ArrayList), typeof(RawListValueAdapter)},
                {typeof(IList), typeof(RawListValueAdapter)},
                {typeof(ICollection), typeof(RawListValueAdapter)},
                {typeof(IEnumerable), typeof(RawListValueAdapter)},
                #endif
            };
            if (type.IsArray)
            {
                elementTypes = new[]{ type.GetElementType() };
                adapterType = typeof(GenericArrayValueAdapter<>);
            }
            else if (introspector.GetGenericTypeDefinition() is IGenericTypeIntrospector genericIntrospector)
            {
                var genericDefinitionType = genericIntrospector.GenericDefinitionType;
                var dictValueTypes = introspector.GetInterfaces()
                    .Select(i => i.GetGenericTypeDefinition())
                    .Where(i => i != null && i.GenericDefinitionType == typeof(IDictionary<,>))
                    .Select(x => x.GenericTypeArguments)
                    .SingleOrDefault();
                elementTypes = dictValueTypes ?? new[]{ genericIntrospector.GenericTypeArguments[0] };
                collectionType = dictValueTypes != null ? typeof(IDictionary<,>) : genericDefinitionType;
            }
            else
            {
                // raw collections do not need element type
                elementTypes = null;
            }

            if (adapterType == null && !adapterTypesMap.TryGetValue(collectionType, out adapterType))
            {
                return null;
            }

            var adapterIntrospector = elementTypes != null 
                ? new TypeIntrospector(adapterType)
                    .GetGenericTypeDefinition()
                    .MakeGenericType(elementTypes)
                    .Introspect()
                : new TypeIntrospector(adapterType);

            return CreateInstance(adapterIntrospector) as IDocumentCollectionValueAdapter;
        }
    }
}
#endif