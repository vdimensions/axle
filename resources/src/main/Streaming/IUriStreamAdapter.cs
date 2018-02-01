using System;
using System.IO;


namespace Axle.Resources.Streaming
{
    public interface IUriStreamAdapter
    {
        Stream GetStream(Uri uri);
    }
}