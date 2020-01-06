#if NETSTANDARD1_5_OR_NEWER || NET35_OR_NEWER
using System;
using System.Linq;
using Axle.Reflection;
using Axle.Reflection.Extensions.Type;
using Axle.Verification;

namespace Axle.Conversion.Binding
{
    /// <summary>
    /// An implementation of the <see cref="IBindingObjectInfoProvider"/> that uses reflection.
    /// </summary>
    public sealed class ReflectionObjectInfoProvider : IBindingObjectInfoProvider
    {
        /// <inheritdoc/>
        public IReadWriteMember GetMember(object instance, string member)
        {
            return GetMembers(instance).SingleOrDefault(x => StringComparer.Ordinal.Equals(member, (string) x.Name));
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
            var introspector = new TypeIntrospector(instance.GetType());
            return introspector
                .GetMembers(ScanOptions.Instance | ScanOptions.Public)
                .Select(x => x as IReadWriteMember)
                .Where(x => x != null)
                .ToArray();
        }

        /// <inheritdoc/>
        public object CreateInstance(Type type)
        {
            try
            {
                Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
                var introspector = new TypeIntrospector(type);
                return introspector.GetConstructor(ScanOptions.Instance | ScanOptions.Public)?.Invoke();
            }
            catch
            {
                return null;
            }
        }
    }
}
#endif