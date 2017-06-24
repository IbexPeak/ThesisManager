namespace ThesisManager.Web.Areas {
    /// <summary>
    /// Schnittstelle die eine Klasse beschreibt, die einen Nutzer am System anmelden und abmelden kann.
    /// </summary>
    public interface IAuthenticationManager {

        /// <summary>
        /// Markiert einen Nutzer als am System angemeldet.
        /// </summary>
        /// <param name="username">Der Name des Nutzers, der am System angemeldet werden soll.</param>
        void Login(string username);
        
        
        /// <summary>
        /// Hebt die Anmeldung eines Nutzers auf und meldet den Nutzer ab.
        /// </summary>
        /// <param name="username">Der Name des Nutzers, der vom System abgemeldet werden soll.</param>
        void Logout(string username);

    }
}

