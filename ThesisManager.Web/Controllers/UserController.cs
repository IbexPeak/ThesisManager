namespace ThesisManager.Web.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using ThesisManager.Core.Domain;
    using ThesisManager.Core.Service;
    using ThesisManager.Web.Models.User;

    /// <summary>
    ///     Controller für alles was Nutzer betrifft.
    /// </summary>
    public class UserController : CrudController {
        /// <summary>
        ///     Liefert oder setzt den <see cref="IUserService" />.
        /// </summary>
        public IUserService UserService { get; set; }

        /// <summary>
        ///     Liefert die Nutzer-erstellen-Seite.
        /// </summary>
        /// <param name="login">Der Login von dem LDAP-Nutzer, zu dem ein DB-Eintrag generiert werden soll</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(string login) {
            try {
                User user = UserService.Get(login);

                UserViewModel viewModel = new UserViewModel(user);

                return View("CreateUser", viewModel);
            } catch (Exception ex) {
                return ModalError(OnCreateException(ex));
            }
        }

        /// <summary>
        ///     Liefert eine PartialView, auf der alle Domain-Objekte des Typs <see cref="User" /> angezeigt werden.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Grid() {
            try {
                IList<UserListModel> users = UserService.GetAll().Select(u => new UserListModel(u)).ToList();
                return PartialView("ListUsers", users);
            } catch (Exception ex) {
                Logger.Error(string.Format("Fehler beim Anzeigen aller Elemente vom Typ {0} in der Grid-Ansicht.", typeof(User)), ex);
                throw;
            }
        }
    }
}