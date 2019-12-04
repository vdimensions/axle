#if NETSTANDARD1_5_OR_NEWER || NET35_OR_NEWER
using System;
using System.Linq;
using Axle.Reflection;
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
            return GetMembers(instance)
                .Where(x => StringComparer.Ordinal.Equals(member, x.Name))
                .SingleOrDefault();
        }

        /// <inheritdoc/>
        public IReadWriteMember[] GetMembers(object instance)
        {
            var introspector = new DefaultIntrospector(instance.GetType());
            return introspector
                .GetMembers(ScanOptions.Instance | ScanOptions.Public)
                .Select(x => x as IReadWriteMember)
                .Where(x => x != null)
                .ToArray();
        }
        /// <inheritdoc/>
        public bool TryCreateInstance(Type type, out object instance)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(type, nameof(type)));
            var introspector = new DefaultIntrospector(type);
            instance = introspector.GetConstructor(ScanOptions.Instance | ScanOptions.Public)?.Invoke();
            return instance != null;
        }
    }
}
#endif