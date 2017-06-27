namespace ThesisManager.Web {
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;

    using Common.Logging;

    using Spring.Context.Support;
    using Spring.Web.Mvc;

    using ThesisManager.Core.Service;
    using ThesisManager.Web.Controllers;

    /// <summary>
    /// Haupteinstiegspunkt der Anwendung.
    /// </summary>
    public class MvcApplication : SpringMvcApplication {
        protected void Application_Error(object sender, EventArgs e) {
            ShowCustomErrorPage(Server.GetLastError());
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e) {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null) {
                try {
                    string username = User.Identity.Name;
                    IUserService userService = ContextRegistry.GetContext().GetObject("userService") as IUserService;
                    HttpContext.Current.User = userService.Get(username);
                } catch (Exception) {
                    /* Generischen Forms Principal lassen */
                }
            }
        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ShowCustomErrorPage(Exception exception) {
            HttpException httpException = exception as HttpException;
            if (httpException == null)
                httpException = new HttpException(500, "Internal Server Error", exception);

            Response.Clear();
            Response.ContentType = "text/html; charset=UTF-8";
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("fromAppErrorEvent", true);

            switch (httpException.GetHttpCode()) {
                case (int)HttpStatusCode.Unauthorized:
                    routeData.Values.Add("action", "Unauthorized");
                    break;
                case (int)HttpStatusCode.Forbidden:
                    routeData.Values.Add("action", "Forbidden");
                    break;

                case (int)HttpStatusCode.NotFound:
                    routeData.Values.Add("action", "NotFound");
                    break;

                case (int)HttpStatusCode.InternalServerError:
                    routeData.Values.Add("action", "ServerError");
                    LogManager.GetLogger<MvcApplication>().Error("Interner Anwendungsfehler!", exception);
                    break;

                default:
                    LogManager.GetLogger<MvcApplication>().Error("Http-Status-Code-Fehler", exception);
                    routeData.Values.Add("action", "OtherHttpStatusCode");
                    routeData.Values.Add("httpStatusCode", httpException.GetHttpCode());
                    break;
            }

            Server.ClearError();

            IController controller = ContextRegistry.GetContext().GetObject("errorController") as ErrorController;
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}