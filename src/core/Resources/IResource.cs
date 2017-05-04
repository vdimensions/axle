using System;
using System.Globalization;
using System.IO;
using System.Net.Mime;


namespace Axle.Resources
{
    public interface IResource
    {
		Stream Open();

		string Bundle { get; }
		string Name { get; }
		Uri Uri { get; }
        CultureInfo Culture { get; }
        ContentType ContentType { get; }
        object Value { get; }
    }

    public interface IResource<T> : IResource
    {
        new T Value { get; }
    }
}
