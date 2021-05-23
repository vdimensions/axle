using System.IO;
using System.Text;

namespace Axle.Text.Documents
{
    /// <summary>
    /// A text document reader which interprets a certain text format into a structured textual data.
    /// Different implementations of the <see cref="ITextDocumentReader"/> are used to target different text formats,
    /// such as JSON, XML, Yaml and etc.  
    /// </summary>
    public interface ITextDocumentReader
    {
        /// <summary>
        /// Reads the provided <paramref name="stream"/> of data, applying the specified <paramref name="encoding"/> and
        /// produces an instance of <see cref="ITextDocumentRoot"/> representing the interpreted document structure.
        /// </summary>
        /// <param name="stream">
        /// A <see cref="Stream"/> containing the document contents.  
        /// </param>
        /// <param name="encoding">
        /// An instance of <see cref="Encoding"/> which is used to convert the streamed data into a textual format.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ITextDocumentRoot"/> representing the interpreted document structure.
        /// </returns>
        ITextDocumentRoot Read(Stream stream, Encoding encoding);
        
        /// <summary>
        /// Reads the provided raw text <paramref name="document"/> and produces an instance of
        /// <see cref="ITextDocumentRoot"/> representing the interpreted document structure.
        /// </summary>
        /// <param name="document">
        /// A <see cref="string"/> representing the document contents.  
        /// </param>
        /// <returns>
        /// An instance of <see cref="ITextDocumentRoot"/> representing the interpreted document structure.
        /// </returns>
        ITextDocumentRoot Read(string document);

        /// <summary>
        /// Reads the provided raw text <paramref name="document"/> and produces an instance of
        /// <see cref="ITextDocumentRoot"/> representing the interpreted document structure.
        /// </summary>
        /// <param name="document">
        /// An <see cref="ITextDocumentAdapter"/> implementation representing the raw document contents.  
        /// </param>
        /// <returns>
        /// An instance of <see cref="ITextDocumentRoot"/> representing the interpreted document structure.
        /// </returns>
        ITextDocumentRoot Read(ITextDocumentAdapter document);
    }
}