using Axle.Modularity;

namespace Axle.Data
{
    internal sealed class ReportsToDataModuleAttribute : ReportsToAttribute
    {
        public ReportsToDataModuleAttribute() : base(typeof(DataModule)) { }
    }
}