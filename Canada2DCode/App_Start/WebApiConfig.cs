using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

namespace Canada2DCode
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "userlogin",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional },
               constraints: new { controller = "userlogin" }
           );

            config.Routes.MapHttpRoute(
               name: "Register",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional },
               constraints: new { controller = "DataApi" }
           );

            config.Routes.MapHttpRoute(
                name: "Email",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "CustomerData",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}
