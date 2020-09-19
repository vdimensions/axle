using Axle.Modularity;

namespace Axle.Data
{
    internal sealed class ProvidesForDataModuleAttribute : ProvidesForAttribute
    {
        public ProvidesForDataModuleAttribute() : base(typeof(DataModule)) { }
    }
}