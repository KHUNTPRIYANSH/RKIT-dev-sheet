using FinalDemo.Models.POCO;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace FinalDemo
{
    /// <summary>
    /// This class represents the Web API application and handles application startup and initialization.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The method that is called when the application starts.
        /// It configures Web API routes, sets up database connections, and initializes the database.
        /// </summary>
        protected void Application_Start()
        {
            // Configures Web API routes using WebApiConfig class
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Read the connection string from Web.config for MySQL connection
            var connectionstring = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            // Create a global OrmLiteConnectionFactory using the connection string and MySQL dialect provider
            var _dbfactory = new OrmLiteConnectionFactory(connectionstring, MySqlDialect.Provider);

            // Store the DbFactory globally in Application state for future use
            Application["Dbfactory"] = _dbfactory;

            // Automatically create tables at application startup if they do not exist
            InitializeDatabase(_dbfactory);
        }

        /// <summary>
        /// Initializes the database by creating necessary tables if they don't already exist.
        /// </summary>
        /// <param name="dbFactory">The OrmLite connection factory used to open a database connection.</param>
        private void InitializeDatabase(OrmLiteConnectionFactory dbFactory)
        {
            // Open a database connection using the factory
            using (var db = dbFactory.OpenDbConnection())
            {
                // Create tables if they do not exist for each of the POCO classes (USR01, EMP01, DEPT01)
                db.CreateTableIfNotExists<USR01>();
                db.CreateTableIfNotExists<EMP01>();
                db.CreateTableIfNotExists<DEPT01>();
            }
        }
    }
}
