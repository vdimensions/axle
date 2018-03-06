using System;
using System.ComponentModel;
using System.Globalization;

using Axle.Configuration.Sdk;
using Axle.Text.RegularExpressions;


namespace Axle.Configuration
{
    /// <summary>
    /// A configuration converter class that can handle <see cref="AssemblyPathExpression" /> instances.
    /// </summary>
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
