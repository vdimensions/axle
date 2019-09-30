namespace Axle.Data.DataSources
{
    [RequiresDataSources]
    public interface IDataSourceProvider
    {
        void RegisterDataSources(IDataSourceRegistry dataSourceRegistry);
    }
}