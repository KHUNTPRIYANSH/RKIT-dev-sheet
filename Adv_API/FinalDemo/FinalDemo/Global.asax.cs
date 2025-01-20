
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
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

           
            // Read the connection string from Web.config
            var connectionstring = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            // Create a global OrmLiteConnectionFactory
            var _dbfactory = new OrmLiteConnectionFactory(connectionstring, MySqlDialect.Provider);

            // Store the DbFactory globally in Application state
            Application["Dbfactory"] = _dbfactory;

            // Automatically create tables at application startup
            InitializeDatabase(_dbfactory);
        }

        private void InitializeDatabase(OrmLiteConnectionFactory dbFactory)
        {
            using (var db = dbFactory.OpenDbConnection())
            {
               
               db.CreateTableIfNotExists<USR01>();
               db.CreateTableIfNotExists<EMP01>();
               db.CreateTableIfNotExists<DEPT01>();

            }
        }

       
    }

 }



