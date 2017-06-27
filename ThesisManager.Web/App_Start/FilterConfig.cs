namespace ThesisManager.Web {
    using System.Web.Mvc;

    /// <summary>
    /// Klasse zur Konfiguration der Filter.
    /// </summary>
    public class FilterConfig {
        /// <summary>
        /// Registriert die globalen Filter.
        /// </summary>
        /// <param name="filters">Die Liste der Filter</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}