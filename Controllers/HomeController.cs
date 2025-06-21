using System.Web.Mvc;

namespace MathCaptchaBatch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult BulkCaptchas() => View();
        public ActionResult BatchPartial(int count = 15) => PartialView();
    }
}
