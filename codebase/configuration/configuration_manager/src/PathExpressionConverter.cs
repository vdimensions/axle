#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Axle.Configuration.ConfigurationManager.Sdk;
using Axle.Text.Expressions.Path;

namespace Axle.Configuration.ConfigurationManager
{
    /// <summary>
    /// A configuration converter class that can handle <see cref="PathExpression" /> instances.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class PathExpressionConverter : ConfigurationConverter<PathExpression>
    {
        protected override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, PathExpression value, Type destinationType)
        {
            if (destinationType == typeof(PathExpression))
            {
                return value;
            }
            if (destinationType == typeof(string))
            {
                return value.Pattern;
            }
            throw new NotSupportedException(string.Format("Cannot convert an instance of {0} to type {1}.", value.GetType().FullName, destinationType.FullName));
        }

        protected override PathExpression ConvertFrom(ITypeDescriptorContext ctx, CultureInfo culture, string value) => new PathExpression(value);
    }
}
#endif