namespace ThesisManager.Web.Controllers {
    using System.Collections.Generic;
    using System.Web.Mvc;

    using ThesisManager.Web.Models.Menu;

    /// <summary>
    /// Controller für alles was die Anwendung selbst betrifft. Zb. "Über", "Kontakt", etc.
    /// </summary>
    public class ApplicationController : BaseController {
        /// <summary>
        /// Über-Seite.
        /// </summary>
        /// <returns></returns>
        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Kontakt-Seite.
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Index-Seite
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            return View();
        }

        /// <summary>
        ///     Methode die das Hauptmenü rendert/liefert.
        ///     Dazu wird anhand der Rechte überprüft, was der Nutzer darf und das Menü dementsprechend angepasst.
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuPartial() {
            ActionUrl activeLink = GetActiveLink();

            IList<MenuItemViewModel> menuModel = new List<MenuItemViewModel>();

            MenuViewModel menuViewModel = new MenuViewModel(menuModel);

            return View("MenuPartial", menuViewModel);
        }
    }
}