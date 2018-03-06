using System.Configuration;
using System.Reflection;
using System.Xml;

using Axle.Extensions.Type;


namespace Axle.Configuration.Sdk
{
    //[Maturity(CodeMaturity.Stable)]
    public abstract class AbstractConfigurationElementParser : IConfigurationElementParser
    {
        public abstract bool Accept(string elementName);

        public abstract ConfigurationElement Parse(XmlReader reader, bool serializeCollectionKey);
    }
    //[Maturity(CodeMaturity.Stable)]
    public abstract class AbstractConfigurationElementParser<T> : AbstractConfigurationElementParser, IConfigurationElementParser<T>
        where T: ConfigurationElement, new()
    {
        static readonly DeserializeMethod<T> DeserializeMethod;
        static AbstractConfigurationElementParser()
        {
            var type = typeof (T);
            if (type.ExtendsOrImplements<ISupportsDeserialize>())
            {
                DeserializeMethod = (e, r, b) => ((ISupportsDeserialize) e).Deserialize(r, b);
            }
            else if (type.ExtendsOrImplements<ISupportsDeserializeInternal>())
            {
                DeserializeMethod = (e, r, b) => ((ISupportsDeserializeInternal) e).Deserialize(r, b);
            }
            else 
            {
                var deserializeMethod = typeof(ConfigurationElement).GetMethod("DeserializeElement", BindingFlags.Instance|BindingFlags.NonPublic);
                DeserializeMethod = (e, r, b) => deserializeMethod.Invoke(e, new object[] { r, b });
            }
        }

        //[Maturity(CodeMaturity.Unstable)]
        private static T DoParse(XmlReader reader, bool serializeCollectionKey)
        {
            var element = new T();
            DeserializeMethod(element, reader, serializeCollectionKey);
            return element;
        }


        //[Maturity(CodeMaturity.Frozen)]
        public sealed override ConfigurationElement Parse(XmlReader reader, bool serializeCollectionKey)
        {
            return DoParse(reader, serializeCollectionKey);
        }

        //[Maturity(CodeMaturity.Frozen)]
        T IConfigurationElementParser<T>.Parse(XmlReader reader, bool serializeCollectionKey)
        {
            return DoParse(reader, serializeCollectionKey);
        }
    }
}