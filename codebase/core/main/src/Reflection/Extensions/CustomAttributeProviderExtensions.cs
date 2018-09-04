using System;
using System.Collections.Generic;
using System.Linq;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

using Axle.Verification;


namespace Axle.Reflection.Extensions
{
    #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
    using ICustomAttributeProvider = System.Reflection.ICustomAttributeProvider;
    #endif

    internal static class CustomAttributeProviderExtensions
    {
        private struct AttributeInfo : IAttributeInfo
        {
            public Attribute Attribute { get; internal set; }
            public AttributeTargets AttributeTargets { get; internal set; }
            public bool AllowMultiple { get; internal set; }
            public bool Inherited { get; internal set; }
        }

        #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
        internal static IAttributeInfo[] GetEffectiveAttributes(this ICustomAttributeProvider provider, System.Type attributeType = null)
        {
            provider.VerifyArgument(nameof(provider)).IsNotNull();

            var notInherited = (attributeType == null ? provider.GetCustomAttributes(false) : provider.GetCustomAttributes(attributeType, false))
                    #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
                    .Cast<Attribute>()
                    #endif
                    ;
            var inherited = (attributeType == null ? provider.GetCustomAttributes(true) : provider.GetCustomAttributes(attributeType, true))
                    #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
                    .Cast<Attribute>()
                    #endif
                    ;
            return FilterAttributes(notInherited, inherited);
        }
        #elif NETSTANDARD || NET45_OR_NEWER
        internal static IAttributeInfo[] GetEffectiveAttributes(this MemberInfo provider, System.Type attributeType = null)
        {
            provider.VerifyArgument(nameof(provider)).IsNotNull();

            var notInherited = (attributeType == null ? provider.GetCustomAttributes(false) : provider.GetCustomAttributes(attributeType, false))
                    #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
                    .Cast<Attribute>()
                    #endif
                    ;
            var inherited = (attributeType == null ? provider.GetCustomAttributes(true) : provider.GetCustomAttributes(attributeType, true))
                    #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
                    .Cast<Attribute>()
                    #endif
                    ;
            return FilterAttributes(notInherited, inherited);
        }

        internal static IAttributeInfo[] GetEffectiveAttributes(this ParameterInfo provider, System.Type attributeType = null)
        {
            provider.VerifyArgument(nameof(provider)).IsNotNull();

            var notInherited = (attributeType == null ? provider.GetCustomAttributes(false) : provider.GetCustomAttributes(attributeType, false))
                    #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
                    .Cast<Attribute>()
                    #endif
                    ;
            var inherited = (attributeType == null ? provider.GetCustomAttributes(true) : provider.GetCustomAttributes(attributeType, true))
                    #if NETSTANDARD1_5_OR_NEWER || NETFRAMEWORK
                    .Cast<Attribute>()
                    #endif
                    ;
            return FilterAttributes(notInherited, inherited);
        }
        #endif

        internal static IAttributeInfo[] FilterAttributes(IEnumerable<Attribute> notInherited, IEnumerable<Attribute> inherited)
        {
            var comparer = EqualityComparer<Attribute>.Default;
            var result = new List<IAttributeInfo>();
            foreach (var attribute in notInherited)
            {
                #if NETSTANDARD || NET45_OR_NEWER
                var attributeUsage = attribute.GetType().GetTypeInfo().GetCustomAttributes(typeof(AttributeUsageAttribute), false).Cast<AttributeUsageAttribute>().Single();
                #else
                var attributeUsage = attribute.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute), false).Cast<AttributeUsageAttribute>().Single();
                #endif
                result.Add(
                    new AttributeInfo
                    {
                        Attribute = attribute,
                        Inherited = false,
                        AllowMultiple = attributeUsage.AllowMultiple,
                        AttributeTargets = attributeUsage.ValidOn,
                    });
            }
            foreach (var attribute in inherited)
            {
                if (result.Any(x => comparer.Equals(x.Attribute, attribute)))
                {
                    continue;
                }
                #if NETSTANDARD || NET45_OR_NEWER
                var attributeUsage = attribute.GetType().GetTypeInfo().GetCustomAttributes(typeof(AttributeUsageAttribute), false).Cast<AttributeUsageAttribute>().Single();
                #else
                var attributeUsage = attribute.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute), false).Cast<AttributeUsageAttribute>().Single();
                #endif
                result.Add(
                    new AttributeInfo
                    {
                        Attribute = attribute,
                        Inherited = true,
                        AllowMultiple = attributeUsage.AllowMultiple,
                        AttributeTargets = attributeUsage.ValidOn,
                    });
            }
            return result.ToArray();
        }
    }
}
