using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Canada2DCode
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "customer",
                url: "{controller}/{action}/{id}",
              
                defaults: new { controller = "Home", action = "Customer" },
                namespaces: new[] { "Canada2DCode.Controllers" }

                );

            routes.MapRoute(
                name: "product",
                url: "product/{*catchall}",
                defaults: new { controller = "Home", action = "Product" },
                namespaces: new[] { "Canada2DCode.Controllers" }
                );

           
            routes.MapRoute(
               name: "ScanQRCode",
               url: "ScanQRCode/{*catchall}",
               defaults: new { controller = "Home", action = "ScanQRCode" },
               namespaces: new[] { "Canada2DCode.Controllers" }
               );

            routes.MapRoute(
              name: "PersonDetail",
              url: "PersonDetail/{*catchall}",
              defaults: new { controller = "PersonDetail", action = "PersonDetail" },
              namespaces: new[] { "Canada2DCode.Controllers" }
              );

            routes.MapMvcAttributeRoutes();

        
        }
    }
}