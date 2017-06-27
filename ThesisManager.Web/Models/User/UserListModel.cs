namespace ThesisManager.Web.Models.User {
    using System;
    using System.ComponentModel.DataAnnotations;

    using ThesisManager.Core.Domain;

    /// <summary>
    ///     Model, welches eine Tabellenzeile in der Nutzertabelle abbildet.
    /// </summary>
    public class UserListModel {
        /// <summary>
        /// Erstellt ein neues <see cref="UserListModel"/> anhand eines Nutzers
        /// </summary>
        /// <param name="user"></param>
        public UserListModel(User user) {
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            Login = user.Login;
            Firstname = user.Firstname;
            Lastname = user.Lastname;

            UserType = user.UserType;
        }

        /// <summary>
        ///     Ruft den Vornamen des Nutzers ab.
        /// </summary>
        [Display(Name = "Vorname")]
        public string Firstname { get; private set; }

        /// <summary>
        ///     Ruft den Nachname des Nutzers ab.
        /// </summary>
        [Display(Name = "Nachname")]
        public string Lastname { get; private set; }

        /// <summary>
        /// Ruft den Login des Nutzers ab.
        /// </summary>
        [Display(Name = "Login")]
        public string Login { get; private set; }

        /// <summary>
        ///     Ruft den Vornamen des Nutzers ab oder legt diesen fest.
        /// </summary>
        [Display(Name = "Nutzertyp")]
        public UserType UserType { get; private set; }
    }
}