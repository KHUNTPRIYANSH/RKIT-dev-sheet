using Adv_Final.Models.POCO;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Adv_Final
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // Database connection using connection string and orm lite tool.
            var connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            string[] connStr = connectionString.Split
                (';');
            string[] dbStr = connStr[1].Split('=');
            
            // Storing OrmLiteConnectionFactory instance for further usage in any other component.
            
            Application["DbFactory"] = dbFactory;

            // Following code will create table automatically if table not their in DB
            InitilizeDB(dbFactory , dbStr[1]);
        }

        private void InitilizeDB(OrmLiteConnectionFactory dbFactory , string schema)
        {
            using(var db = dbFactory.OpenDbConnection())
            {
                db.CreateSchema(schema);
                db.CreateTableIfNotExists<Emp01>();
            }
        }
    }
}
