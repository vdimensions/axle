using Axle.Text.Documents;
using Axle.Verification;

namespace Axle.Configuration.Text.Documents
{
    public class TextDocumentConfigSource : AbstractTextDocumentConfigSource
    {
        private readonly ITextDocumentReader _documentReader;
        private readonly string _rawDocument;

        public TextDocumentConfigSource(ITextDocumentReader documentReader, string rawDocument)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(documentReader, nameof(documentReader)));
            Verifier.IsNotNull(Verifier.VerifyArgument(rawDocument, nameof(rawDocument)));
            _documentReader = documentReader;
            _rawDocument = rawDocument;
        }

        protected sealed override ITextDocumentRoot ReadDocument() => _documentReader.Read(_rawDocument);
    }
}