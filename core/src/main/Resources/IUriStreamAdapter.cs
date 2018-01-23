using System;
using System.IO;


namespace Axle.Resources
{
    public interface IUriStreamAdapter
    {
        Stream GetStream(Uri uri);
    }
}