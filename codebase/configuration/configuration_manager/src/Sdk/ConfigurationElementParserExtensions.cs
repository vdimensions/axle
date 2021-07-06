#if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
using System.Configuration;
using System.Xml;
using Axle.Verification;

namespace Axle.Configuration.ConfigurationManager.Sdk
{
    internal static class ConfigurationElementParserExtensions
    {
        public static ConfigurationElement Parse(this IConfigurationElementParser parser, XmlReader reader)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(parser, nameof(parser)));
            return parser.Parse(reader, false);
        }
        public static T Parse<T>(this IConfigurationElementParser<T> parser, XmlReader reader) where T: ConfigurationElement
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(parser, nameof(parser)));
            return parser.Parse(reader, false);
        }
    }
}
#endif