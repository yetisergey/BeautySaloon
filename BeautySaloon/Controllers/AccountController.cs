namespace BeautySaloon.Controllers
{
    using Models;
    using System.Web.Mvc;
    using System.Web.Security;
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model.Login == "admin" && model.Password == "admin")
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}