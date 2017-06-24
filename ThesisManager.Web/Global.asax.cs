namespace ThesisManager.Web {
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Spring.Web.Mvc;

    public class MvcApplication : SpringMvcApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}