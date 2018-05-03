using System;
using System.Collections.Generic;
using System.Linq;

using Axle.Core.Infrastructure.DependencyInjection.Descriptors;
using Axle.Verification;


namespace Axle.Core.Infrastructure.DependencyInjection.Sdk
{
    /// <summary>
    /// A class representing a construction recepie; that is the constructor/factory method, 
    /// properties and fields of an object type that need to be populated by a 
    /// <see cref="IContainer">dependency container</see> during the object construction.
    /// </summary>
    public class ConstructionRecepie
    {
        private sealed class FactoryDescriptorComparer : IComparer<IFactoryDescriptor>
        {
            public int Compare(IFactoryDescriptor x, IFactoryDescriptor y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }
                if (ReferenceEquals(null, y))
                {
                    return -1;
                }
                if (ReferenceEquals(null, x))
                {
                    return 1;
                }
                var argsCountDiff = x.Arguments.Count(arg => !arg.Optional) - y.Arguments.Count(arg => !arg.Optional);
                //if (argsCountDiff == 0)
                //{
                //    if (x.Factory is IMethod)
                //    {
                //        return 1;
                //    }
                //    if (y.Factory is IMethod)
                //    {
                //        return -1;
                //    }
                //}
                return argsCountDiff;
            }
        }

        public static IEnumerable<ConstructionRecepie> ForType<T>(IDependencyDescriptorProvider dependencyDescriptorProvider)
        {
            return ForType(typeof(T), dependencyDescriptorProvider);
        }
        public static IEnumerable<ConstructionRecepie> ForType(Type type, IDependencyDescriptorProvider dependencyDescriptorProvider)
        {
            type.VerifyArgument(nameof(type)).IsNotNull();
            dependencyDescriptorProvider.VerifyArgument(nameof(dependencyDescriptorProvider)).IsNotNull();

            var factories = dependencyDescriptorProvider.GetFactories(type).OrderByDescending(x => x, new FactoryDescriptorComparer());
            var candidateProperties = dependencyDescriptorProvider.GetProperties(type).ToArray();
            var candidateFields = dependencyDescriptorProvider.GetFields(type).ToArray();

            foreach (var factory in factories)
            {
                var selectedProperties = FilterDescriptors(factory, candidateProperties, dependencyDescriptorProvider);
                var selectedFields = FilterDescriptors(factory, candidateFields, dependencyDescriptorProvider);
                yield return new ConstructionRecepie(type, factory, selectedProperties, selectedFields);
            }
        }

        private static IEnumerable<T> FilterDescriptors<T>(IFactoryDescriptor descriptor, IEnumerable<T> descriptors, IDependencyDescriptorProvider dependencyDescriptorProvider)
            where T: class, IDependencyDescriptor
        {
            var descriptorsCondensed = new HashSet<T>(descriptors, new ReferenceEqualityComparer<T>());
            foreach (var d in descriptors)
            foreach (var arg in descriptor.Arguments)
            {
                if (dependencyDescriptorProvider.DoDependenciesConverge(d.Info, arg.Info))
                {
                    descriptorsCondensed.Remove(d);
                }
            }
            return descriptorsCondensed;
        }

        private ConstructionRecepie(
                Type targetType, 
                IFactoryDescriptor factory, 
                IEnumerable<IPropertyDependencyDescriptor> properties, 
                IEnumerable<IPropertyDependencyDescriptor> fields)
        {
            TargetType = targetType.VerifyArgument(nameof(targetType)).IsNotNull();
            Factory = factory.VerifyArgument(nameof(factory)).IsNotNull().Value;
            Properties = properties.VerifyArgument(nameof(properties)).IsNotNull().Value;
            Fields = fields.VerifyArgument(nameof(fields)).IsNotNull().Value;
        }


        private object ResolveArgument(IDependencyResolver resolver, IFactoryArgumentDescriptor arg)
        {
            try
            {
                return resolver.Resolve(arg.Info);
            }
            catch  (Exception e)
            {
                if (arg.Optional)
                {
                    return arg.DefaultValue;
                }

                switch (e)
                {
                    case DependencyResolutionException _:
                        throw;
                    default:
                        throw new DependencyResolutionException(arg.Info.Type, e);
                }
            }
        }
        private object ResolveProperty(IDependencyResolver resolver, IPropertyDependencyDescriptor prop)
        {
            try
            {
                return resolver.Resolve(prop.Info);
            }
            catch (Exception e)
            {
                if (prop.DefaultValue != null)
                {
                    return prop.DefaultValue;
                }

                switch (e)
                {
                    case DependencyResolutionException _:
                        throw;
                    default:
                        throw new DependencyResolutionException(prop.Info.Type, e);
                }
            }
        }

        public object Complete(IDependencyResolver resolver)
        {
            try
            {
                var arguments = Factory.Arguments.Select((arg, ix) => ResolveArgument(resolver, arg)).ToArray();
                var instance = Factory.CreateInstance(arguments);
                var properties = Fields.Union(Properties).Select(f => new {Prop = f, Value = ResolveProperty(resolver, f)});
                foreach (var property in properties)
                {
                    property.Prop.SetValue(instance, property.Value);
                }

                return instance;
            }
            catch (Exception e)
            {
                throw new DependencyResolutionException(TargetType, e);
            }
        }

        public Type TargetType { get; }
        public IFactoryDescriptor Factory { get; }
        public IEnumerable<IPropertyDependencyDescriptor> Properties { get; }
        public IEnumerable<IPropertyDependencyDescriptor> Fields { get; }
    }
}