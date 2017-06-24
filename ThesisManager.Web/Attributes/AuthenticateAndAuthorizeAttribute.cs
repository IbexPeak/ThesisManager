namespace ThesisManager.Web.Attributes {
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Attribute, dass für einen Request zunächst überprüft, ob der Nutzer angemeldet ist und anschließend überprüft ob der Nutzer authorisiert ist.
    /// 
    /// Ist der Nutzer nicht angemeldet wird eine HttpException mit dem Statuscode 401 (Unauthorized) geworfen.
    /// Ist der Nutzer nicht autorisiert wird eine HttpException mit dem Statuscode 403 (Forbidden) geworfen.
    /// </summary>
    public class AuthenticateAndAuthorizeAttribute : AuthorizeAttribute {

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            if (filterContext.HttpContext.Request.IsAuthenticated) {
                throw new HttpException((int)System.Net.HttpStatusCode.Forbidden, "Keine Autorisierung!");
            } else {
                throw new HttpException((int)System.Net.HttpStatusCode.Unauthorized, "Keine Authentifizierung!");
            }

            base.HandleUnauthorizedRequest(filterContext);
        }

    }
}