#if NETSTANDARD1_5_OR_NEWER || NET35_OR_NEWER
using Axle.Reflection;
using Axle.Verification;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Axle.References
{
    /// <summary>
    /// A class that is used for creating a shallow copy of an object instance.
    /// </summary>
    [SuppressMessage("ReSharper", "ClassCannotBeInstantiated")]
    public sealed class ShallowCopy
    {
        /// <summary>
        /// Gets the sole instance of the <see cref="ShallowCopy"/> class.
        /// </summary>
        private static readonly ShallowCopy Instance = Singleton<ShallowCopy>.Instance.Value;

        private readonly IMethod _memberwiseClone;

        private static bool IsSafeFromSideEffects(ITypeIntrospector introspector)
        {
            switch (introspector.TypeCode)
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Char:
                case TypeCode.String:
                case TypeCode.DateTime:
                    return true;
                default:
                    return introspector.TypeFlags.HasFlag(TypeFlags.ValueType);
            }
        }

        /// <summary>
        /// Determines if a given <paramref name="type"/>'s shallow copy clone is safe from side effects.
        /// A side effect in this context means a modification of either the original object, or its 
        /// cloned instance, which propagates to the other.
        /// The object's shallow copy is determined to be safe when the object is a <see cref="ValueType"/>, 
        /// or all of the object's members are either value types or known immutable objects 
        /// (such as <see cref="string"/>.
        /// </summary>
        /// <param name="type">
        /// The type to determine whether shallow copies of its instances are safe from side effects.
        /// </param>
        /// <returns>
        /// <c>true</c> if shallow copies of an instance of the given <paramref name="type"/> are not
        /// affected by modifications made to the original instance, or other shallow copies of it.
        /// </returns>
        public static bool IsSafeFromSideEffects(Type type)
        {
            var introspector = new TypeIntrospector(type);
            return introspector
                .GetMembers(ScanOptions.Public|ScanOptions.NonPublic|ScanOptions.Instance)
                .Where(m => m is IReadWriteMember)
                .Select(m => new TypeIntrospector(m.MemberType))
                .All(IsSafeFromSideEffects);
        }

        /// <summary>
        /// Creates a shallow copy of the provided object <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance">
        /// The object instance to create a shallow copy from.
        /// </param>
        /// <returns>
        /// A new object which is a shallow copy of the provided object <paramref name="instance"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="instance"/> is <c>null</c>.
        /// </exception>
        public static T Create<T>(T instance)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(instance, nameof(instance)));
            return (T) Instance.DoCreate(instance);
        }

        /// <summary>
        /// Creates a shallow copy of the provided object <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance">
        /// The object instance to create a shallow copy from.
        /// </param>
        /// <returns>
        /// A new object which is a shallow copy of the provided object <paramref name="instance"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="instance"/> is <c>null</c>.
        /// </exception>
        public static object Create(object instance)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(instance, nameof(instance)));
            return Instance.DoCreate(instance);
        }

        private ShallowCopy()
        {
            _memberwiseClone = new TypeIntrospector<object>()
                .GetMethod(ScanOptions.Instance | ScanOptions.NonPublic, nameof(MemberwiseClone));
        }

        private object DoCreate(object obj)
        {
            if (IsSafeFromSideEffects(new TypeIntrospector(obj.GetType())))
            {
                return obj;
            }
            return _memberwiseClone.Invoke(obj);
        }
    }
}
#endif