namespace Axle.Data.DataSources
{
    public interface IDataSourceAware
    {
        IDataSource DataSource { get; }
    }
}