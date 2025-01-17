using ServiceStack.OrmLite;
using System.Configuration;
using System.Web.Http;
using YourNamespace.Models.POCO;

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
            string[] database = connStr[1].Split('=');
            
            // Storing OrmLiteConnectionFactory instance for further usage in any other component.
            
            Application["DbFactory"] = dbFactory;

            // Following code will create table automatically if table not their in DB
            InitilizeDB(dbFactory , database[1]);
        }

        private void InitilizeDB(OrmLiteConnectionFactory dbFactory , string schema)
        {
            using(var db = dbFactory.OpenDbConnection())
            {
                db.CreateTableIfNotExists<USR01>();
                db.CreateTableIfNotExists<EMP01>();
                db.CreateTableIfNotExists<DEPT01>();
            }
        }
    }
}
