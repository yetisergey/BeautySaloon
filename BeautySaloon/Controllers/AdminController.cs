namespace BeautySaloon.Controllers
{
    using System.Web.Mvc;
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Photos()
        {
            return View();
        }
    }
}