using System.Diagnostics.CodeAnalysis;
using Axle.Modularity;

namespace Axle.Data.Npgsql
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [Module]
    [DbServiceProvider(Name = NpgsqlServiceProvider.Name)]
    internal sealed class NpgsqlModule : DbServiceProvider
    {
        public NpgsqlModule() : base(NpgsqlServiceProvider.Instance) { }
    }
}
