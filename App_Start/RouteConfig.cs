using System.Web.Mvc;
using System.Web.Routing;

namespace MathCaptchaBatch
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignore WebResource.axd etc
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 1️⃣ Empty URL: go to Home/BulkCaptchas
            routes.MapRoute(
                name: "Root",
                url: "",
                defaults: new { controller = "Home", action = "BulkCaptchas" }
            );

            // 2️⃣ Default MVC route for everything else
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "BulkCaptchas", id = UrlParameter.Optional }
            );
        }
    }
}
