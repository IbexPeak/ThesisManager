namespace ThesisManager.Web.Controllers {
    using System;
    using System.Net;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using ThesisManager.Web.Attributes;
    using ThesisManager.Web.Models.Menu;
    using ThesisManager.Web.Models.Shared;

    /// <summary>
    /// Diese Controller-Klasse stellt die Basis-Klasse für alle Controller der Webanwendung dar. Jeder in der Anwendung verwendete Controller muss von dieser Klasse erben.
    /// Der Controller beinhaltet die Unterstützung für Mehrsprachigkeit.
    /// </summary>
    [NoCaching]
    public class BaseController : Controller {

        public const string ERROR_ALERT_KEY = "Error";
        public const string MESSAGE_ALERT_KEY = "Message";


        /// <summary>
        /// Fügt im TempDataDictionary einen Eintrag mit dem in <see cref="ERROR_ALERT_KEY"/> hinterlegten Schlüssel und dem übergebenen Text als Wert hinzu.
        /// </summary>
        /// <param name="errorMessage">Der anzuzeigende Fehlertext.</param>
        protected void AlertError(string errorMessage) {
            TempData.Add(ERROR_ALERT_KEY, errorMessage);
        }

        /// <summary>
        /// Fügt im TempDataDictionary einen Eintrag mit dem in <see cref="ERROR_ALERT_KEY"/> hinterlegten Schlüssel und dem übergebenen Text als Wert hinzu.
        /// </summary>
        /// <param name="message">Die anzuzeigende Nachricht.</param>
        protected void AlertMessage(string message) {
            TempData.Add(MESSAGE_ALERT_KEY, message);
        }
        
        /// <summary>
        /// Liefert eine PartialView, die eine Fehler-Meldung in einem Modalen Fenster anzeigt.
        /// Der StatusCode wird auf 500 (Interner Server Fehler) gesetzt.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected ActionResult ModalInternalServerError(ErrorViewModel model) {
            SetInternalServerError();
            return ModalError(model);
        }

        protected ActionResult ModalInternalServerError(string title, string message) {
            return ModalInternalServerError(new ErrorViewModel(title, message));
        }

        protected ActionResult ModalError(string title, string message) {
            return ModalError(new ErrorViewModel(title, message));
        }

        protected ActionResult ModalError(ErrorViewModel model) {
            if (model == null) {
                throw new ArgumentNullException("model");
            }

            return PartialView("ErrorOverlay", model);
        }

        /// <summary>
        /// Zeigt ein Modales Hinweisfenster an.
        /// </summary>
        /// <param name="message">Die Nachricht die dem Nutzer angezeigt werden soll.
        ///</param>
        /// <returns></returns>
        protected ActionResult ModalMessage(string message) {
            return ModalMessage("Hinweis", message);
        }

        ///  <summary>
        ///  Zeigt ein Modales Hinweisfenster an.
        ///  </summary>
        /// <param name="title">Der Titel der Hinweisbox.</param>
        /// <param name="message">Die Nachricht die dem Nutzer angezeigt werden soll. </param>
        ///  <returns></returns>
        protected ActionResult ModalMessage(string title, string message) {
            return PartialView("MessageOverlay", new MessageViewModel(title, message));
        }


        ///  <summary>
        ///  Zeigt eine Seite, auf der ein Hinweis steht an.
        /// Als Titel der Seite wird <see cref="Resources_Web.Views_Common_Hint_Title"/> verwendet.
        ///  </summary>
        /// <param name="message">Die Nachricht die dem Nutzer angezeigt werden soll. </param>
        ///  <returns></returns>
        protected ActionResult Message(string message) {
            return Message("Hinweis", message);
        }

        ///  <summary>
        ///  Zeigt eine Seite, auf der ein Hinweis steht an.
        ///  </summary>
        /// <param name="title">Der Titel der Hinweisseite.</param>
        /// <param name="message">Die Nachricht die dem Nutzer angezeigt werden soll. </param>
        ///  <returns></returns>
        protected ActionResult Message(string title, string message) {
            return PartialView("Message", new MessageViewModel(title, message));
        }

        /// <summary>
        /// Liefert die übergebene Fehlermeldung als Partial.
        /// </summary>
        /// <param name="errorMessage">Die anzuzeigende Fehlermeldung.</param>
        /// <returns></returns>
        protected ActionResult ErrorPartial(string errorMessage) {
            return PartialView("ErrorPartial", new ErrorViewModel(errorMessage));
        }

        /// <summary>
        /// Liefert die übergebene Fehlermeldung als View.
        /// </summary>
        /// <param name="errorTitle">Die Überschrift der Fehlerseite.</param>
        /// <param name="errorMessage">Die anzuzeigende Fehlermeldung.</param>
        /// <returns></returns>
        protected ActionResult Error(string errorTitle, string errorMessage) {
            return View("Error", new ErrorViewModel(errorTitle, errorMessage));
        }

        /// <summary>
        /// Liefert eine PartialView auf der eine Bestätigen-Abfrage angezeigt wird.
        /// </summary>
        /// <param name="model">ViewModel für den Bestätigen Dialog.</param>
        /// <returns></returns>
        protected ActionResult Confirm(ConfirmDialogViewModel model) {
            if (model == null) {
                throw new ArgumentNullException("model");
            }

            return PartialView("ConfirmOverlay", model);
        }

        /// <summary>
        /// Ändert den StatusCode Antwort auf die aktuelle Anfrage auf 500 (Interner Server Fehler).
        /// </summary>
        protected void SetInternalServerError() {
            SetHttpStatusCode(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Ändert den StatusCode Antwort auf die aktuelle Anfrage auf den übergebenen Wert.
        /// </summary>
        /// <param name="code">Der zu setzende Http-Status-Code.</param>
        protected void SetHttpStatusCode(HttpStatusCode code) {

            Response.Clear();
            Response.StatusCode = (int)code;
        }
    
        protected bool IsSameAction(ActionUrl activeLink, string area, string controller, string action) {
            if (activeLink == null) {
                throw new ArgumentNullException("activeLink");
            }

            return activeLink.Equals(area, controller, action);
        }

        protected bool IsSameController(ActionUrl activeLink, string area, string controller) {
            if (activeLink == null) {
                throw new ArgumentNullException("activeLink");
            }

            return activeLink.Equals(area, controller);
        }

        protected ActionUrl GetActiveLink() {
            RouteData routeData = Request.RequestContext.RouteData;
            string area = ActionUrl.ROOT_AREA_NAME;
            if (routeData.DataTokens.ContainsKey("area")) {
                area = routeData.DataTokens["area"].ToString();
            }
            string controller = "Home";
            if (routeData.Values.ContainsKey("controller")) {
                controller = routeData.Values["controller"].ToString();
            }

            string action = "Index";
            if (routeData.Values.ContainsKey("action")) {
                action = routeData.Values["action"].ToString();
            }

            return new ActionUrl(area, controller, action);
        }
    }
}