using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TownComparisons.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "admin",
                url: "admin/{*catchall}",
                defaults: new { controller = "Angular", action = "Home" });

            routes.MapRoute(
                name: "categories",
                url: "categories/{*catchall}",
                defaults: new { controller = "Angular", action = "Category" });

            routes.MapRoute(
                name: "category",
                url: "category/{*catchall}",
                defaults: new { controller = "Angular", action = "Category" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Angular", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
