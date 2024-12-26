using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Swashbuckle.Application;
using Swashbuckle.Swagger;



namespace PhaseThreeWebApiProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enable Swagger
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("api/Product", "My API 1");
                //c.RouteTemplate = "swagger/docs/{apiVersion}";
            })
            .EnableSwaggerUi(); // Enable the Swagger UI

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
