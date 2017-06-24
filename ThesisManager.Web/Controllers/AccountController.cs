namespace ThesisManager.Web.Controllers {
    using System;
    using System.Web.Mvc;

    using ThesisManager.Core.Persistence;
    using ThesisManager.Core.Service;
    using ThesisManager.Web.Areas;
    using ThesisManager.Web.Helper;
    using ThesisManager.Web.Models.Account;

    public class AccountController : BaseController {
        /// <summary>
        ///     Legt das Objekt fest, dass für die Authentifizierung des Nutzers verantwortlich ist.
        /// </summary>
        public IAuthenticationManager AuthenticationManager { get; set; }

        /// <summary>
        ///     Liefert oder setzt das <see cref="TestData" />-Objekt.
        /// </summary>
        public TestData TestData { get; set; }

        /// <summary>
        ///     Liefert oder setzt den <see cref="IUserService" />.
        /// </summary>
        public IUserService UserService { get; set; }

        /// <summary>
        ///     Methode die eine Ansicht liefert, auf welcher ein Nutzer seine Anmeldeinformationen eintragen kann, um sich am
        ///     System anzumelden.
        ///     Für den Aufruf der Methode muss der Anwender nicht angemeldet sein.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login() {
            TestData.GenerateTestData();

            LoginViewModel model = new LoginViewModel();
            return View("Login", model);
        }

        /// <summary>
        ///     Methode, die den Nutzer am System anmeldet, wenn die Anmeldedaten korrekt übergeben wurden.
        ///     Bei Erfolgreicher Anmeldung erfolgt eine Weiterleitung zur Startseite des Adminbereichs. Andernfalls wird die
        ///     Login-Seite erneut angezeigt.
        ///     Das Cookie bleibt für den eingestellten Zeitraum und nur während der Browser-Sitzung aktiv.
        /// </summary>
        /// <param name="credentials">Die Anmeldetdaten des Nutzers mit Nutzername und Kennwort.</param>
        /// <returns>Weiterleitung zur Startseite.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel credentials) {
            try {
                bool canLogin = UserService.CanLogin(credentials.Login, credentials.Password);
                if (!canLogin) {
                    //Logger.Warn("Fehlgeschlagener Anmeldeversuch mit ungültigen Nutzerdaten. [" + credentials.Username + "].");
                    ModelState.AddModelError(credentials.GetPropertyName(m => m.Login), "Ungültige Anmeldedaten!");
                } else {
                    AuthenticationManager.Login(credentials.Login);

                    //Logger.Info("Der Nutzer [" + credentials.Username + "] hat sich angemeldet.");

                    return RedirectToAction("Index", "Thesis");
                }
            } catch (Exception ex) {
                //Logger.Error("Beim Anmelden eines Nutzers [" + credentials.Username + "] ist ein Fehler aufgetreten. ", ex);
                ModelState.AddModelError("", "Fehler bei der Anmeldung");
            }

            LoginViewModel viewModel = new LoginViewModel(credentials.Login);
            return View("Login", viewModel);
        }
    }
}