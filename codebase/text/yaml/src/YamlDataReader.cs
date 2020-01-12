using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Axle.Text.StructuredData.Yaml
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class YamlDataReader : AbstractStructuredDataReader
    {
        private sealed class Adapter : AbstractStructuredDataAdapter
        {
            private readonly IDictionary<string, IStructuredDataAdapter[]> _data;
            private readonly string _value;

            public Adapter(StringComparer comparer, object rawData, string value = null)
            {
                _value = value;
                var data = new Dictionary<string, IStructuredDataAdapter[]>(comparer);
                switch (rawData)
                {
                    case string v:
                        _value = v;
                        break;
                    case IDictionary<object, object> d:
                        foreach (var kvp in d)
                        {
                            var key = kvp.Key.ToString();
                            switch (kvp.Value)
                            {
                                case IList<object> list:
                                    data.Add(key, list.Select(x => new Adapter(comparer, x) as IStructuredDataAdapter).ToArray());
                                    break;
                                default:
                                    data.Add(key, new IStructuredDataAdapter[]{new Adapter(comparer, kvp.Value)});
                                    break;
                            }
                        }
                        break;
                }
                _data = data;
            }

            public override IDictionary<string, IStructuredDataAdapter[]> GetChildren() => _data;
            public override string Value => _value;
        }
        
        public YamlDataReader(StringComparer comparer) : base(comparer) { }

        protected override IStructuredDataAdapter CreateAdapter(Stream stream, Encoding encoding)
        {
            var deserializer = new DeserializerBuilder().Build();
            try
            {
                var result = deserializer.Deserialize<object>(new StreamReader(stream, encoding, true));
                return new Adapter(Comparer, result);
            }
            catch (YamlException e)
            {
                if (e.Message.Contains("Expected 'MappingStart', got 'SequenceStart'"))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    var result = deserializer.Deserialize<List<object>>(new StreamReader(stream, encoding, true));
                    return new Adapter(Comparer, result);
                }
                throw;
            }
        }

        protected override IStructuredDataAdapter CreateAdapter(string data)
        {
            var deserializer = new DeserializerBuilder().Build();
            try
            {
                var result = deserializer.Deserialize<object>(new StringReader(data));
                return new Adapter(Comparer, result);
            }
            catch (YamlException e)
            {
                if (e.Message.Contains("Expected 'MappingStart', got 'SequenceStart'"))
                {
                    var result = deserializer.Deserialize<List<object>>(new StringReader(data));
                    return new Adapter(Comparer, result);
                }
                throw;
            }
        }
    }
}
