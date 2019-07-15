using System.Data;

namespace Axle.Data
{
    public interface IDataSource
    {

    }
    public class DataSource
    {
        private readonly IDbServiceProvider _serviceProvider;
        private readonly string _connectionString;

        public DataSource(IDbServiceProvider serviceProvider, string connectionString)
        {
            _serviceProvider = serviceProvider;
            _connectionString = connectionString;
        }

        public IDbConnection OpenConnection()
        {
            var connection = _serviceProvider.CreateConnection(_connectionString);
            connection.Open();
            return connection;
        }

        //public IDataReader ExecuteReader(IDbConnection conn)
        //{
        //    _serviceProvider.created
        //}
    }
}