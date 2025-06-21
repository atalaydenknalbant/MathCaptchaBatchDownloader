using System.Web.Mvc;
using System.Web.Routing;

namespace MathCaptchaBatch
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("BulkCaptchas", "Home/BulkCaptchas",
                new { controller = "Home", action = "BulkCaptchas" });

            routes.MapRoute("BatchPartial", "Home/BatchPartial",
                new { controller = "Home", action = "BatchPartial" });

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new { controller = "Home", action = "BulkCaptchas", id = UrlParameter.Optional });
        }
    }
}
