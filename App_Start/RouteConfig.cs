using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "MoviesByRealeaseDate", //the name of the route
            //    "movies/released/{year}/{month}", //this is the path to write on browser
            //    new { controller = "Movies", action = "ByReleaseDate" }, //this is the action that executes
            //    new { year = @"\d{4}", month = @"\d{2}"} //this is the format or number of digits that the parameters should have
            //    );

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
