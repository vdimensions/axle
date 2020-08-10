using System;
using System.Collections.Generic;

using Axle.Resources.Extraction;


namespace Axle.Resources.Bundling
{
    public interface IResourceBundleContent
    {
        IEnumerable<Uri> Locations { get; }
        IEnumerable<IResourceExtractor> Extractors { get; }
        
        /// <summary>
        /// Gets the name of the current bundle
        /// </summary>
        string Bundle { get; }
    }
}