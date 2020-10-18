using System;
using System.IO;
using System.Text;
using Axle.References;
using Axle.Text.Documents;
using Axle.Verification;

namespace Axle.Configuration.Text.Documents
{
    public class StreamDocumentConfigSource : AbstractTextDocumentConfigSource
    {
        #if NET35 || !NETSTANDARD2_0_OR_NEWER
        private sealed class LazyRef<T> : IReference<T> where T : class
        {
            private readonly Func<T> _factory;
            private T _value;
            private bool _hasValue = true;

            public LazyRef(Func<T> factory)
            {
                _factory = factory;
            }

            public bool TryGetValue(out T value)
            {
                if (_value == null)
                {
                    try
                    {
                        value = _value = _factory();
                        return value != null;
                    }
                    catch
                    {
                        value = default(T);
                        _hasValue = false;
                        return false;
                    }
                }

                value = _value;
                return  true;
            }

            public T Value => TryGetValue(out var value) ? value : null;

            object IReference.Value => Value;
            bool IReference.HasValue => _hasValue;
        }
        #endif
        
        private readonly ITextDocumentReader _documentReader;
        private readonly Encoding _encoding;
        private readonly IReference<Stream> _lazyStream;

        private StreamDocumentConfigSource(
            ITextDocumentReader documentReader, 
            Encoding encoding, 
            IReference<Stream> lazyStream)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(documentReader, nameof(documentReader)));
            Verifier.IsNotNull(Verifier.VerifyArgument(encoding, nameof(encoding)));
            Verifier.IsNotNull(Verifier.VerifyArgument(lazyStream, nameof(lazyStream)));
            _documentReader = documentReader;
            _encoding = encoding;
            _lazyStream = lazyStream;
        }
        public StreamDocumentConfigSource(
                ITextDocumentReader documentReader, 
                Encoding encoding, 
                Func<Stream> streamFunc)
            : this(
                documentReader, 
                encoding,
                new LazyRef<Stream>(Verifier.IsNotNull(Verifier.VerifyArgument(streamFunc, nameof(streamFunc))).Value))
        {
        }
        public StreamDocumentConfigSource(ITextDocumentReader documentReader, Func<Stream> streamFunc)
            : this(documentReader, Encoding.UTF8, streamFunc) { }

        /// <inheritdoc />
        protected sealed override ITextDocumentRoot ReadDocument()
        {
            var stream = _lazyStream.Value;
            return stream == null ? null : _documentReader.Read(_lazyStream.Value, _encoding);
        }
    }
}