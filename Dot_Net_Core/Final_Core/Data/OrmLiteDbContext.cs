using ServiceStack.OrmLite;
using System.Data;

namespace Final_Core.Data
{
    /// <summary>
    /// Provides the DbContext for accessing the database using OrmLite.
    /// </summary>
    public class OrmLiteDbContext
    {
        #region Fields

        private readonly OrmLiteConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrmLiteDbContext"/> class.
        /// Sets up the connection string and OrmLite connection factory.
        /// </summary>
        /// <param name="configuration">The configuration containing the connection string.</param>
        public OrmLiteDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MyDbConnection");
            _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens a new database connection using OrmLite.
        /// </summary>
        /// <returns>An open <see cref="IDbConnection"/>.</returns>
        public IDbConnection OpenDbConnection() => _dbFactory.OpenDbConnection();

        #endregion
    }
}
