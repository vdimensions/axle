using System.Configuration;
using System.Reflection;
using System.Xml;


namespace Axle.Configuration.Sdk
{
    public abstract class AbstractConfigurationElementParser : IConfigurationElementParser
    {
        public abstract bool Accept(string elementName);

        public abstract ConfigurationElement Parse(XmlReader reader, bool serializeCollectionKey);
    }

    public abstract class AbstractConfigurationElementParser<T> : AbstractConfigurationElementParser, IConfigurationElementParser<T>
        where T: ConfigurationElement, new()
    {
        static readonly DeserializeMethod<T> DeserializeMethod;

        static AbstractConfigurationElementParser()
        {
            var type = typeof (T);
            if (typeof(ISupportsDeserialize).IsAssignableFrom(type))
            {
                DeserializeMethod = (e, r, b) => ((ISupportsDeserialize) e).Deserialize(r, b);
            }
            else if (typeof(ISupportsDeserializeInternal).IsAssignableFrom(type))
            {
                DeserializeMethod = (e, r, b) => ((ISupportsDeserializeInternal) e).Deserialize(r, b);
            }
            else 
            {
                var deserializeMethod = typeof(ConfigurationElement).GetMethod("DeserializeElement", BindingFlags.Instance|BindingFlags.NonPublic);
                if (deserializeMethod != null)
                {
                    DeserializeMethod = (e, r, b) => deserializeMethod.Invoke(e, new object[] { r, b });
                }
            }
        }

        private static T DoParse(XmlReader reader, bool serializeCollectionKey)
        {
            var element = new T();
            DeserializeMethod(element, reader, serializeCollectionKey);
            return element;
        }


        public sealed override ConfigurationElement Parse(XmlReader reader, bool serializeCollectionKey)
        {
            return DoParse(reader, serializeCollectionKey);
        }

        T IConfigurationElementParser<T>.Parse(XmlReader reader, bool serializeCollectionKey)
        {
            return DoParse(reader, serializeCollectionKey);
        }
    }
}