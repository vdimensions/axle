using Axle.Text;
using System;
using System.Collections.Generic;

namespace Axle.Text.Properties
{
    public class PropertiesData : ITextData
    {
        public IEnumerable<ITextDataToken> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITextDataToken> GetChildren(string name)
        {
            throw new NotImplementedException();
        }

        public string Name => throw new NotImplementedException();
    }
}
