using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "",
                new { Controller = "Product", Action = "List", category = (string)null, page =1 });

            routes.MapRoute(null, "Page{page}", 
                new { controller = "Product", action = "List",
                    category = (string)null }, new { page = @"\d+" });

            routes.MapRoute(null, "{category}", 
                new { controller = "Product", action = "List", page = 1 });

            routes.MapRoute(null, "{category}/Page{page}", 
                new { controller = "Product", action = "List" }, new { page = @"\d+" });

            routes.MapRoute(null, "{controller}/{action}");

            /*
             /             Lists the first page of products from all categories 
             /Page2        Lists the specified page (in this case, page 2), showing items from all categories 
             /Soccer       Shows the first page of items from a specific category (in this case, the Soccer category) 
             /Soccer/Page2 Shows the specified page (in this case, page 2) of items from the specified category (in this case, Soccer)
             */



        }
    }
}
