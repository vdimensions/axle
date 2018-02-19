using System;
using System.IO;


namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An interface that is used to obtain a <see cref="Stream">data stream</see> from an <see cref="Uri"/>. 
    /// </summary>
    public interface IUriStreamAdapter
    {
        /// <summary>
        /// Opens <see cref="Stream"/> instance for the data represented by the provided by the <paramref name="uri"/> parameter location.
        /// </summary>
        /// <param name="uri">
        /// A <see cref="Uri"/> that represents the resource to be streamed.
        /// </param>
        /// <returns>
        /// A <see cref="Stream"/> instance for the data represented by the provided by the <paramref name="uri"/> parameter location.
        /// </returns>
        Stream GetStream(Uri uri);
    }
}