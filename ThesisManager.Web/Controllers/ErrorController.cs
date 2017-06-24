namespace ThesisManager.Web.Controllers {
    using System.Web.Mvc;

    using Common.Logging;

    using ThesisManager.Web.Areas;
    using ThesisManager.Web.Models.Shared;

    public class ErrorController : Controller {
        public IAuthenticationManager AuthenticationManager { private get; set; }

        private ILog Logger {
            get { return LogManager.GetLogger(typeof(ErrorController)); }
        }

        public ActionResult Forbidden() {
            Logger.Warn("Einem Nutzer wurde der Zugriff auf eine bestimmte Seite verboten.", Server.GetLastError());

            if (Request.IsAjaxRequest()) {
                return PartialView("ErrorOverlay",
                                   new ErrorViewModel("Verboten", "Einem Nutzer wurde der Zugriff auf eine bestimmte Seite verboten."));
            }

            return View("Error", new ErrorViewModel("Verboten", "Einem Nutzer wurde der Zugriff auf eine bestimmte Seite verboten."));
        }

        public ActionResult Index() {
            return View("Error", new ErrorViewModel("Fehler", "Unbekannter Fehler"));
        }

        public ActionResult NotFound() {
            Logger.Warn("Ein Nutzer hat eine Seite aufgerufen, die nicht gefunden wurde.", Server.GetLastError());

            if (Request.IsAjaxRequest()) {
                return PartialView("ErrorPartial", new ErrorViewModel("Nicht gefunden"));
            }

            return View("Error", new ErrorViewModel("Nicht gefunden", "Ein Nutzer hat eine Seite aufgerufen, die nicht gefunden wurde."));
        }

        public ActionResult OtherHttpStatusCode(int httpStatusCode) {
            Logger.Warn(string.Format("Ein Nutzer hat eine Anfrage gestellt, die zu einem Http-Fehler [{0}] geführt hat.", httpStatusCode),
                        Server.GetLastError());

            if (Request.IsAjaxRequest()) {
                return PartialView("ErrorPartial", new ErrorViewModel("Unbekannter Fehler"));
            }

            return View("Error",
                        new ErrorViewModel("Unbekannter Fehler",
                                           "Ein Nutzer hat eine Anfrage gestellt, die zu einem Http-Fehler [{0}] geführt hat."));
        }

        public ActionResult ServerError() {
            Logger.Error("Es ist ein Server-Fehler aufgetreten.", Server.GetLastError());

            if (Request.IsAjaxRequest()) {
                return PartialView("ErrorPartial", new ErrorViewModel("Unbekannter Fehler"));
            }

            return View("Error",
                        new ErrorViewModel("Unbekannter Fehler",
                                           "Ein Nutzer hat eine Anfrage gestellt, die zu einem Http-Fehler [{0}] geführt hat."));
        }

        public ActionResult Unauthorized() {
            Logger.Warn("Ein Nutzer wollte auf eine Seite zugreifen, ohne angemeldet zu sein.", Server.GetLastError());

            if (User != null) {
                AuthenticationManager.Logout(User.Identity.Name);
            }

            if (Request.IsAjaxRequest()) {
                return PartialView("ErrorPartial",
                                   new ErrorViewModel("Nicht authorisiert", "Sie sind nicht authorisiert für diese Operation!"));
            }

            return RedirectToAction("Login", "Account");
        }
    }
}