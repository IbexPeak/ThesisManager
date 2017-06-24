namespace ThesisManager.Web.Models.Menu {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model, dass den Inhalt des eines Menüs abbildet.
    /// </summary>
    public class MenuViewModel {
        private readonly IList<MenuItemViewModel> _menuItems = new List<MenuItemViewModel>();

        public MenuViewModel(IList<MenuItemViewModel> menuItemViewModels) {
            if (menuItemViewModels == null) {
                throw new ArgumentNullException("menuItemViewModels");
            }
            _menuItems = menuItemViewModels;
        }

        /// <summary>
        /// Ruft die MenuItems ab.
        /// </summary>
        public IList<MenuItemViewModel> MenuItems {
            get { return _menuItems; }
            
        }
    }
}