namespace ThesisManager.Web {
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    ///     Klasse zur Konfiguration der Routen.
    /// </summary>
    public class RouteConfig {
        /// <summary>
        ///     Registriert die Routen
        /// </summary>
        /// <param name="routes">Die Liste der Routen</param>
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Hier wird auch der Startkontroller + Action definiert
            routes.MapRoute(name: "Default",
                            url: "{controller}/{action}/{id}",
                            defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional });
        }
    }
}