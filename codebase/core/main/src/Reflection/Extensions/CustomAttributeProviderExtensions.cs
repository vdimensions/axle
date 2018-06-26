using System;
using System.Collections.Generic;
using System.Linq;
#if NETSTANDARD || NET45_OR_NEWER
using System.Reflection;
#endif

using Axle.Verification;


namespace Axle.Reflection.Extensions
{
    #if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
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

        #if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
        internal static IAttributeInfo[] GetEffectiveAttributes(this ICustomAttributeProvider provider, System.Type attributeType = null)
        {
            provider.VerifyArgument(nameof(provider)).IsNotNull();

            var comparer = EqualityComparer<Attribute>.Default;
            var notInherited = (attributeType == null ? provider.GetCustomAttributes(false) : provider.GetCustomAttributes(attributeType, false))
                    #if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
                    .Cast<Attribute>()
                    #endif
                    ;
            var inherited = (attributeType == null ? provider.GetCustomAttributes(true) : provider.GetCustomAttributes(attributeType, true))
                    #if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
                    .Cast<Attribute>()
                    #endif
                    .Except(notInherited, comparer);
            return FilterAttributes(notInherited, inherited);
        }
        #elif NETSTANDARD || NET45_OR_NEWER
        internal static IAttributeInfo[] GetEffectiveAttributes(this MemberInfo provider, System.Type attributeType = null)
        {
            provider.VerifyArgument(nameof(provider)).IsNotNull();

            var comparer = EqualityComparer<Attribute>.Default;
            var notInherited = (attributeType == null ? provider.GetCustomAttributes(false) : provider.GetCustomAttributes(attributeType, false))
                    #if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
                    .Cast<Attribute>()
                    #endif
                    ;
            var inherited = (attributeType == null ? provider.GetCustomAttributes(true) : provider.GetCustomAttributes(attributeType, true))
                    #if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
                    .Cast<Attribute>()
                    #endif
                    .Except(notInherited, comparer);
            return FilterAttributes(notInherited, inherited);
        }

        internal static IAttributeInfo[] GetEffectiveAttributes(this ParameterInfo provider, System.Type attributeType = null)
        {
            provider.VerifyArgument(nameof(provider)).IsNotNull();

            var comparer = EqualityComparer<Attribute>.Default;
            var notInherited = (attributeType == null ? provider.GetCustomAttributes(false) : provider.GetCustomAttributes(attributeType, false))
                    #if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
                    .Cast<Attribute>()
                    #endif
                    ;
            var inherited = (attributeType == null ? provider.GetCustomAttributes(true) : provider.GetCustomAttributes(attributeType, true))
                    #if NETSTANDARD1_5_OR_NEWER || !NETSTANDARD
                    .Cast<Attribute>()
                    #endif
                    .Except(notInherited, comparer);
            return FilterAttributes(notInherited, inherited);
        }
        #endif

        private static IAttributeInfo[] FilterAttributes(IEnumerable<Attribute> notInherited, IEnumerable<Attribute> inherited)
        {
            var attr = notInherited
                .Select(
                    x => new
                    {
                        Attribute = x,
                        Inherited = false,
                        #if NETSTANDARD || NET45_OR_NEWER
                        AttributeUsage = x.GetType().GetTypeInfo().GetCustomAttributes(typeof(AttributeUsageAttribute), false).Cast<AttributeUsageAttribute>().Single()
                        #else
                        AttributeUsage = x.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute), false).Cast<AttributeUsageAttribute>().Single()
                        #endif
                    })
                .Union(
                    inherited
                        .Select(
                            x => new
                            {
                                Attribute = x,
                                Inherited = true,
                                #if NETSTANDARD || NET45_OR_NEWER
                                AttributeUsage = x.GetType().GetTypeInfo().GetCustomAttributes(typeof(AttributeUsageAttribute), false).Cast<AttributeUsageAttribute>().Single()
                                #else
                                AttributeUsage = x.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute), false).Cast<AttributeUsageAttribute>().Single()
                                #endif
                            })
                    // Only allow inherited attributes that are defined as inheritable
                    .Where(x => x.AttributeUsage.Inherited))
                .Select(
                    x => new AttributeInfo
                    {
                        Attribute = x.Attribute,
                        AllowMultiple = x.AttributeUsage.AllowMultiple,
                        AttributeTargets = x.AttributeUsage.ValidOn,
                        Inherited = x.Inherited
                    } as IAttributeInfo);
            return attr.ToArray();
        }
    }
}
