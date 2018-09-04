#if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Linq;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

using Axle.DependencyInjection.Descriptors;
using Axle.Reflection;
using Axle.Verification;


namespace Axle.DependencyInjection.Sdk
{
    public class DefaultDependencyDescriptorProvider : AbstractDependencyDescriptorProvider
    {
        private const ScanOptions MemberScanOptions = ScanOptions.Instance|ScanOptions.Public|ScanOptions.NonPublic;
        private const ScanOptions FactoryScanOptions = ScanOptions.Static|ScanOptions.Public;

        protected override string GetDependencyName(IField field)
        {
            return string.Empty;
        }
        protected override string GetDependencyName(IProperty property)
        {
            return string.Empty;
        }
        protected override string GetDependencyName(IParameter argument)
        {
            return string.Empty;
        }

        public override IEnumerable<IPropertyDependencyDescriptor> GetFields(Type type)
        {
            return new DefaultIntrospector(type.VerifyArgument(nameof(type)).IsNotNull())
                .GetFields(MemberScanOptions)
                .Where(x => x.AccessModifier != AccessModifier.Private)
                .Where(x => !x.IsReadOnly)
                .Select(GetDescriptor);
        }
        public override IEnumerable<IPropertyDependencyDescriptor> GetProperties(Type type)
        {
            return new DefaultIntrospector(type.VerifyArgument(nameof(type)).IsNotNull())
                .GetProperties(MemberScanOptions)
                .Where(x => x.AccessModifier != AccessModifier.Private)
                .Where(x => x is IWriteableMember)
                .Select(GetDescriptor);
        }

        public override IEnumerable<IFactoryDescriptor> GetFactories(Type type)
        {
            var introspector = new DefaultIntrospector(type.VerifyArgument(nameof(type)).IsNotNull());
            var staticFactories = introspector.GetMethods(FactoryScanOptions)
                #if NETSTANDARD || NET45_OR_NEWER
                .Where(x => type.GetTypeInfo().IsAssignableFrom(x.ReturnType))
                #else
                .Where(x => type.IsAssignableFrom(x.ReturnType))
                #endif
                .Select(GetDescriptor);
            var constructors = introspector.GetConstructors(MemberScanOptions).Select(GetDescriptor);
            return staticFactories.Union(constructors);
        }
    }
}
#endif