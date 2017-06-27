namespace ThesisManager.Web.Models.Account {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Bildet das VM für den Login ab.
    /// </summary>
    public class LoginViewModel {
        /// <summary>
        ///     Konstruktor ohne Initialisierung der Properties.
        /// </summary>
        public LoginViewModel() {
        }

        /// <summary>
        ///     Konstruktor zur Initialisierung mit einem Login.
        /// </summary>
        /// <param name="login"></param>
        public LoginViewModel(string login) {
            Login = login;
        }

        /// <summary>
        ///     Liefert oder setzt den Login.
        /// </summary>
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        /// <summary>
        ///     Liefert oder setzt das Passwort.
        /// </summary>
        [Required]
        [PasswordPropertyText]
        [Display(Name = "Passwort")]
        public string Password { get; set; }
    }
}