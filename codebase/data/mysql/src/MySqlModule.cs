using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.MySql
{
    [Module]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class MySqlModule : DatabaseServiceProviderModule
    {
        public MySqlModule() : base(MySqlServiceProvider.Instance) { }
    }
}