using ServiceStack.OrmLite;
using System.Data;

namespace Final_Core.Data
{
    public class OrmLiteDbContext
    {
        private readonly OrmLiteConnectionFactory _dbFactory;

        public OrmLiteDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MyDbConnection");
            _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }

        // Use synchronous version of OpenDbConnection
        public IDbConnection OpenDbConnection() => _dbFactory.OpenDbConnection();
    }
}
