namespace Axle.Data.DataSources
{
    public interface IDataSource : IDbParameterBuilder
    {
        IDataSourceConnection OpenConnection();

        string DialectName { get; }
        string ConnectionString { get; }
    }
}