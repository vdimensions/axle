#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Axle.Configuration.Legacy.Sdk;
using Axle.Text.Expressions.Path;

namespace Axle.Configuration.Legacy
{
    /// <summary>
    /// A configuration converter class that can handle <see cref="AssemblyPathExpression" /> instances.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class AssemblyPathConverter : ConfigurationConverter<AssemblyPathExpression>
	{
        protected override AssemblyPathExpression ConvertFrom(ITypeDescriptorContext ctx, CultureInfo culture, string value)
        {
            var result = new AssemblyPathExpression(value);
            return result;
        }

        protected override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, AssemblyPathExpression value, Type destinationType)
        {
            return value.Pattern;
        }
	}
}
#endif