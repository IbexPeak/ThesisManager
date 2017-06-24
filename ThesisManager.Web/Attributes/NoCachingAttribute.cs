namespace ThesisManager.Web.Attributes {
    using System;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Attribute, welches das Caching von Antworten deaktiviert.
    /// Das Attribute kann entweder für einen Controller oder ActionMethode definiert werden.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NoCachingAttribute : ActionFilterAttribute {
        public override void OnResultExecuting(ResultExecutingContext filterContext) {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }
}