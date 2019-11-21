using Axle.Modularity;

namespace Axle.Data
{
    internal sealed class UtilizedByDataModuleAttribute : UtilizedByAttribute
    {
        public UtilizedByDataModuleAttribute() : base(typeof(DataModule)) { }
    }
}