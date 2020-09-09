﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Axle.Text.Documents.Yaml
{
    /// <summary>
    /// An <see cref="ITextDocumentAdapter"/> implementation capable of handling yaml files.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class YamlDocumentReader : AbstractTextDocumentReader
    {
        private sealed class Adapter : AbstractTextDocumentAdapter
        {
            [SuppressMessage("ReSharper", "CognitiveComplexity")]
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
                        //
                        // Special handling of character lists, they could contain a secure string and we do not want to 
                        // create a managed string instance
                        //
                        if (list.All(x => x is string s && s.Length == 1))
                        {
                            yield return new Adapter(
                                name, 
                                Enumerable.Empty<ITextDocumentAdapter>(), 
                                CharSequence.Create(list.Select(x => x.ToString()[0])));
                            break;
                        }
                        foreach (var adapter in list.SelectMany(x => ToChildren(name, x)))
                        {
                            yield return adapter;
                        }
                        break;
                }
            }

            private Adapter(string name, IEnumerable<ITextDocumentAdapter> children, CharSequence value)
            {
                Key = name;
                Children = children;
                Value = value;
            }

            public override string Key { get; }
            public override CharSequence Value { get; }
            public override IEnumerable<ITextDocumentAdapter> Children { get; }
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="YamlDocumentReader"/> class using the provided <paramref name="comparer"/>.
        /// </summary>
        /// <param name="comparer">
        /// An <see cref="IEqualityComparer{T}"/> that is used to compare document node keys.
        /// </param>
        public YamlDocumentReader(IEqualityComparer<string> comparer) : base(comparer) { }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override ITextDocumentAdapter CreateAdapter(string document)
        {
            var deserializer = new DeserializerBuilder().Build();
            try
            {
                var result = deserializer.Deserialize<object>(new StringReader(document));
                return Adapter.ToChildren(string.Empty, result).SingleOrDefault();
            }
            catch (YamlException e)
            {
                if (!e.Message.Contains("Expected 'MappingStart', got 'SequenceStart'"))
                {
                    throw;
                }
                var result = deserializer.Deserialize<List<object>>(new StringReader(document));
                return Adapter.ToChildren(string.Empty, result).SingleOrDefault();
            }
        }
    }
}
