using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CRUD_MYSQL_DB_WEB_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            // Option 1 : Enable CORS globally
            
            var cors = new EnableCorsAttribute("*", "*", "*"); // Allow all origins, headers, and methods
            config.EnableCors(cors);
            // ex
            // EnableCorsAttribute("https://somthing , https://other , .. ,. ", "*", "GET,POST,....")

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
