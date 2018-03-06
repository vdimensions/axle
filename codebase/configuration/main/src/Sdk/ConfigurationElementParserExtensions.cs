﻿using System.Configuration;
using System.Xml;

using Axle.Verification;


namespace Axle.Configuration.Sdk
{
    internal static class ConfigurationElementParserExtensions
    {
        public static ConfigurationElement Parse(this IConfigurationElementParser parser, XmlReader reader)
        {
            return parser.VerifyArgument(nameof(parser)).IsNotNull().Value.Parse(reader, false);
        }
        public static T Parse<T>(this IConfigurationElementParser<T> parser, XmlReader reader) where T: ConfigurationElement
        {
            return parser.VerifyArgument(nameof(parser)).IsNotNull().Value.Parse(reader, false);
        }
    }
}