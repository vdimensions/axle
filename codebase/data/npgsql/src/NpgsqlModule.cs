using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;


namespace Axle.Data.Npgsql
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    internal sealed class NpgsqlModule : DatabaseServiceProviderModule
    {
        public NpgsqlModule() : base(NpgsqlServiceProvider.Instance) { }
    }
}
