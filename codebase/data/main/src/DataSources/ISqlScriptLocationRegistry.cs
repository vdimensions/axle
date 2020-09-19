using System;
using System.Reflection;

namespace Axle.Data.DataSources
{
    /// <summary>
    /// A registry to keep locations of sql script files. The registry is
    /// internally used by <see cref="IDataSourceConnection"/> implementations in order
    /// to resolve dialect-specific sql scripts.
    /// </summary>
    /// <seealso cref="IDataSource"/>
    /// <seealso cref="IDataSourceConnection"/>
    /// <seealso cref="IDataSource.GetScript"/>
    public interface ISqlScriptLocationRegistry
    {
        ISqlScriptLocationRegistry Register(string bundle, Uri location);
        ISqlScriptLocationRegistry Register(string bundle, Assembly assembly, string path);
    }
}