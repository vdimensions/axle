using System;
using System.Collections.Generic;
using System.Linq;

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

        public override IEnumerable<FieldDependencyDescriptor> GetFields(Type type)
        {
            return new DefaultIntrospector(type.VerifyArgument(nameof(type)).IsNotNull())
                .GetFields(MemberScanOptions)
                .Where(x => !x.IsReadOnly)
                .Select(GetDescriptor);
        }
        public override IEnumerable<PropertyDependencyDescriptor> GetProperties(Type type)
        {
            return new DefaultIntrospector(type.VerifyArgument(nameof(type)).IsNotNull())
                .GetProperties(MemberScanOptions)
                .Where(x => x.SetAccessor != null)
                .Select(GetDescriptor);
        }

        public override IEnumerable<FactoryDescriptor> GetFactories(Type type)
        {
            var introspector = new DefaultIntrospector(type.VerifyArgument(nameof(type)).IsNotNull());
            var staticFactories = introspector.GetMethods(FactoryScanOptions)
                .Where(x => type.IsAssignableFrom(x.ReturnType))
                .Select(GetDescriptor);
            var constructors = introspector.GetConstructors(MemberScanOptions)
                .Select(GetDescriptor);
            return staticFactories.Union(constructors);
        }
    }
}