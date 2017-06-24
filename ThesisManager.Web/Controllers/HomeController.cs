namespace ThesisManager.Web.Controllers {
    using System.Web.Mvc;

    public class HomeController : Controller {
        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Index() {
            return View();
        }
    }
}