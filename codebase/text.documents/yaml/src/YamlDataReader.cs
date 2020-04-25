using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Axle.Text.Documents.Yaml
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class YamlDataReader : AbstractTextDocumentReader
    {
        private sealed class Adapter : AbstractTextDocumentAdapter
        {
            internal static IEnumerable<ITextDocumentAdapter> ToChildren(string name, object o)
            {
                switch (o)
                {
                    case string v:
                        yield return new Adapter(name, Enumerable.Empty<ITextDocumentAdapter>(), v);
                        break;
                    case IDictionary<object, object> dict:
                        var children = dict.SelectMany(x => ToChildren(x.Key.ToString(), x.Value));
                        yield return new Adapter(name, children, null);
                        break;
                    case IList<object> list:
                        foreach (var adapter in list.SelectMany(x => ToChildren(name, x)))
                        {
                            yield return adapter;
                        }
                        break;
                }
            }

            private Adapter(string name, IEnumerable<ITextDocumentAdapter> children, string value)
            {
                Key = name;
                Children = children;
                Value = value;
            }

            public override string Key { get; }
            public override string Value { get; }
            public override IEnumerable<ITextDocumentAdapter> Children { get; }
        }
        
        public YamlDataReader(StringComparer comparer) : base(comparer) { }

        protected override ITextDocumentAdapter CreateAdapter(Stream stream, Encoding encoding)
        {
            var deserializer = new DeserializerBuilder().Build();
            try
            {
                var result = deserializer.Deserialize<object>(new StreamReader(stream, encoding, true));
                return Adapter.ToChildren(string.Empty, result).SingleOrDefault();
            }
            catch (YamlException e)
            {
                if (!e.Message.Contains("Expected 'MappingStart', got 'SequenceStart'"))
                {
                    throw;
                }
                stream.Seek(0, SeekOrigin.Begin);
                var result = deserializer.Deserialize<List<object>>(new StreamReader(stream, encoding, true));
                return Adapter.ToChildren(string.Empty, result).SingleOrDefault();
            }
        }

        protected override ITextDocumentAdapter CreateAdapter(string data)
        {
            var deserializer = new DeserializerBuilder().Build();
            try
            {
                var result = deserializer.Deserialize<object>(new StringReader(data));
                return Adapter.ToChildren(string.Empty, result).SingleOrDefault();
            }
            catch (YamlException e)
            {
                if (!e.Message.Contains("Expected 'MappingStart', got 'SequenceStart'"))
                {
                    throw;
                }
                var result = deserializer.Deserialize<List<object>>(new StringReader(data));
                return Adapter.ToChildren(string.Empty, result).SingleOrDefault();
            }
        }
    }
}
