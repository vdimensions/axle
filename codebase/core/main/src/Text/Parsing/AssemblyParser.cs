using System;
using System.Reflection;
using Axle.Environment;

namespace Axle.Text.Parsing
{
    /// <summary>
    /// A class that can parse <see cref="string">string</see> representations of an
    /// <see cref="Assembly">assembly</see> to a valid <see cref="Assembly"/> instance.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public sealed class AssemblyParser : AbstractParser<Assembly>
    {
        /// <inheritdoc />
        protected override Assembly DoParse(CharSequence value, IFormatProvider formatProvider)
        {
            return Platform.Runtime.LoadAssembly(value.ToString());
        }
    }
}
