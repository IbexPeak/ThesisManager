namespace ThesisManager.Web.Models.Account {
    public class LoginViewModel {
        public LoginViewModel() {
        }

        public LoginViewModel(string login) {
            Login = login;
        }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}