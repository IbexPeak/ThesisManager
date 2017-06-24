namespace ThesisManager.Web.Controllers {
    using System.Collections.Generic;
    using System.Web.Mvc;

    using ThesisManager.Web.Models.Menu;

    public class ApplicationController : BaseController {
        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index() {
            return View();
        }

        /// <summary>
        ///     Methode die das Hauptmenü des Administrationsbereich rendert/liefert.
        ///     Dazu wird anhand der Rechte überprüft, was der Nutzer darf und das Menü dementsprechend angepasst.
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuPartial() {
            ActionUrl activeLink = GetActiveLink();

            IList<MenuItemViewModel> menuModel = new List<MenuItemViewModel>();
            //menuModel.Add(new SimpleMenuItemViewModel("menu_item_home", new ActionLink(Resources_Web.Views_Common_AdminMenu_Labels_Dashboard, "Admin", "AdminHome", "Index"), IsSameAction(activeLink, "AdminHome", "Home", "Index"), true));
            //menuModel.Add(new DropDownMenuItemViewModel("dropdown_reporting", Resources_Web.Views_Common_AdminMenu_Labels_Reporting, IsSameController(activeLink, "Admin", "Reporting"),
            //                                            new List<MenuItemViewModel> {
            //                                                new SimpleMenuItemViewModel("menu_item_reporting_basedata", new ActionLink(Resources_Web.Views_Common_AdminMenu_Labels_Reporting_BaseData,"Admin", "Reporting", "BaseData"), IsSameAction(activeLink, "Admin", "Reporting", "BaseData"), isAdmin),
            //                                                new SimpleMenuItemViewModel("menu_item_reporting_teams", new ActionLink(Resources_Web.Views_Common_AdminMenu_Labels_Reporting_Teams, "Admin", "Reporting", "Index"), IsSameAction(activeLink, "Admin", "Reporting", "Index")),
            //                                                new SimpleMenuItemViewModel("menu_item_reporting_translations", new ActionLink(Resources_Web.Views_Common_AdminMenu_Labels_Reporting_Translations,"Admin", "Reporting", "ReportTranslations"), IsSameAction(activeLink, "Admin", "Reporting", "ReportTranslations"))
            //                                            }));

            //menuModel.Add(new SeparatorMenuItemViewModel());

            MenuViewModel menuViewModel = new MenuViewModel(menuModel);

            return View("MenuPartial", menuViewModel);
        }
    }
}