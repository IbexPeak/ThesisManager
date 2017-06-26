namespace ThesisManager.Web.Models.Account {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel {
        public LoginViewModel() {
        }

        public LoginViewModel(string login) {
            Login = login;
        }

        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [PasswordPropertyText]
        [Display(Name = "Passwort")]
        public string Password { get; set; }
    }
}