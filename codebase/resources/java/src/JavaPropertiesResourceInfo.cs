using System.Collections.Generic;
using System.Globalization;
using System.IO;

using Kajabity.Tools.Java;


namespace Axle.Resources.Java
{
    public class JavaPropertiesResourceInfo : ResourceInfo
    {
        private Dictionary<string, string> _data;

        internal JavaPropertiesResourceInfo(string name, CultureInfo culture, string contentType, Dictionary<string, string> data) 
            : base(name, culture, contentType)
        {
            _data = data;
        }

        public override Stream Open()
        {
            var result = new MemoryStream();
            var writer = new JavaPropertyWriter(_data);
            writer.Write(result, null);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        public IDictionary<string, string> Data => _data;
    }
}