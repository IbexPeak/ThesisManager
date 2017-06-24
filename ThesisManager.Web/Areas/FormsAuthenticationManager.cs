namespace ThesisManager.Web.Areas {
    using System;
    using System.Web.Security;

    using Common.Logging;

    /// <summary>
    ///     AuthenticationManager der die Formsauthentifizierung nutzt und zum anmelden eines Nutzers eine Cookie erzeugt. Die
    ///     Anmeldung bleibt für die Dauer der Browsersitzung aktiv.
    /// </summary>
    public class FormsAuthenticationManager : IAuthenticationManager {
        ILog Logger {
            get { return LogManager.GetLogger(typeof(FormsAuthenticationManager)); }
        }

        public void Login(string username) {
            if (string.IsNullOrWhiteSpace(username)) {
                throw new ArgumentNullException("username");
            }

            FormsAuthentication.SetAuthCookie(username, false);
        }

        public void Logout(string username) {
            if (string.IsNullOrWhiteSpace(username)) {
                Logger.Info("Es sollte ein Nutzer abgemeldet werden, obwohl kein Nutzer mehr angemeldet ist.");
                return;
            }

            try {
                FormsAuthentication.SignOut();
            } catch (Exception ex) {
                Logger.Error(string.Format("Nutzer [{0}] konnte nicht abgemeldet werden.", username), ex);
            }
        }
    }
}