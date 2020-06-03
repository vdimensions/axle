#if NETSTANDARD1_5_OR_NEWER || NET35_OR_NEWER
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

        internal abstract class RawCollectionAdapter< TCollection> : CollectionAdapter
            where TCollection : class, IEnumerable
        {
            protected RawCollectionAdapter() : base(typeof(TCollection), typeof(object)) { }

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

        internal abstract class GenericCollectionAdapter<T, TCollection> : CollectionAdapter
            where TCollection: class, IEnumerable<T>
        {
            protected GenericCollectionAdapter() : base(typeof(TCollection), typeof(T)) { }

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

        internal abstract class AbstractGenericListAdapter<T, TList> : GenericCollectionAdapter<T, TList> where TList: class, IList<T>
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

        internal sealed class GenericArrayAdapter<T> : AbstractGenericListAdapter<T, T[]>
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

        internal sealed class GenericListAdapter<T> : AbstractGenericListAdapter<T, List<T>>
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

        internal sealed class GenericLinkedListAdapter<T> : GenericCollectionAdapter<T, LinkedList<T>>
        {
            public override LinkedList<T> SetItems(LinkedList<T> collection, IEnumerable<T> items)
            {
                if (collection != null)
                {
                    collection.Clear();
                } 
                else
                {
                    collection = new LinkedList<T>();
                }
                foreach (var item in items)
                {
                    collection.AddLast(item);
                }
                return collection;
            }
        }

        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        internal sealed class RawListAdapter : RawCollectionAdapter<ArrayList>
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
                    return new IReadWriteMember[0]; 
                case object obj:
                    var introspector = new TypeIntrospector(obj.GetType());
                    var flags = introspector.TypeFlags;
                    var unusableObjectFlags = new[]
                    {
                        TypeFlags.Delegate,
                        TypeFlags.Enum,
                        TypeFlags.NullableValueType,
                        TypeFlags.Attribute
                    };
                    if (unusableObjectFlags.Any(x => flags.HasFlag(x)))
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

        public IBindingCollectionAdapter GetCollectionAdapter(Type type)
        {
            var introspector = new TypeIntrospector(type);
            Type collectionType = type, elementType = null, adapterType = null;
            var adapterTypesMap = new Dictionary<Type, Type>()
            {
                {typeof(List<>), typeof(GenericListAdapter<>)},
                {typeof(ICollection<>), typeof(GenericListAdapter<>)},
                {typeof(IEnumerable<>), typeof(GenericListAdapter<>)},
                {typeof(LinkedList<>), typeof(GenericLinkedListAdapter<>)},
                #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
                {typeof(ArrayList), typeof(RawListAdapter)},
                {typeof(IList), typeof(RawListAdapter)},
                {typeof(ICollection), typeof(RawListAdapter)},
                {typeof(IEnumerable), typeof(RawListAdapter)},
                #endif
            };
            if (type.IsArray)
            {
                elementType = type.GetElementType();
                adapterType = typeof(GenericArrayAdapter<>);
            }
            else if (introspector.GetGenericTypeDefinition() is IGenericTypeIntrospector genericIntrospector)
            {
                var genericDefinitionType = genericIntrospector.GenericDefinitionType;
                collectionType = genericDefinitionType;
                elementType = genericIntrospector.GenericTypeArguments[0];
            }
            else
            {
                // raw collections do not need element type
                elementType = null;
            }

            if (adapterType == null && !adapterTypesMap.TryGetValue(collectionType, out adapterType))
            {
                return null;
            }

            var adapterIntrospector = elementType != null 
                ? new TypeIntrospector(adapterType)
                    .GetGenericTypeDefinition()
                    .MakeGenericType(elementType)
                    .Introspect()
                : new TypeIntrospector(adapterType);

            return CreateInstance(adapterIntrospector) as IBindingCollectionAdapter;
        }
    }
}
#endif