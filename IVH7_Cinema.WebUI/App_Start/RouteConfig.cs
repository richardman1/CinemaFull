using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.WebUI
{
    public class RouteConfig
    {
        [ExcludeFromCodeCoverage] 
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Cinema", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
