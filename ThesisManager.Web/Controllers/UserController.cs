namespace ThesisManager.Web.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using ThesisManager.Core.Domain;
    using ThesisManager.Core.Service;
    using ThesisManager.Web.Models.User;

    public class UserController : CrudController {
        /// <summary>
        ///     Liefert oder setzt den <see cref="IUserService" />.
        /// </summary>
        public IUserService UserService { get; set; }

        [HttpGet]
        public ActionResult Create(string login) {
            try {
                User user = UserService.Get(login);

                //AssertCanCreate();
                UserViewModel viewModel = new UserViewModel(user);

                return View("CreateUser", viewModel);
            } catch (Exception ex) {
                return ModalError(OnCreateException(ex));
            }
        }
        /// <summary>
        ///     Liefert eine PartialView, auf der alle Domain-Objekte des Typs <see cref="TEntity"/> angezeigt werden.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Grid()
        {
            try
            {
                IList<UserListModel> users = UserService.GetAll().Select(u => new UserListModel(u)).ToList();
                return PartialView("ListUsers", users);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Fehler beim Anzeigen aller Elemente vom Typ {0} in der Grid-Ansicht.", typeof(User)), ex);
                throw;
            }

        }
    }
}