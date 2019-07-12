using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;


namespace Axle.Data.Npgsql
{
    [Module]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal sealed class NpgsqlModule : DatabaseServiceProviderModule
    {
        public NpgsqlModule() : base(NpgsqlServiceProvider.Instance) { }
    }
}
