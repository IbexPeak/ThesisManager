namespace ThesisManager.Core.Service {
    using System.Collections.Generic;

    using ThesisManager.Core.Domain;
    using ThesisManager.Core.Service.Core;

    /// <summary>
    ///     Schnittstelle für einen Service zur Verwaltung von <see cref="User" />.
    /// </summary>
    public interface IUserService : IDomainEntityWithIdService<User> {
        /// <summary>
        ///     Prüft ob ein Nutzer eingeloggt werden kann.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <param name="password">Das Password</param>
        /// <returns></returns>
        bool CanLogin(string login, string password);

        /// <summary>
        ///     Methode zum Erstellen eines Nutzers in der DB.
        /// </summary>
        /// <param name="login">Der Login des Nutzers</param>
        /// <param name="userType">Der Typ des Nutzers</param>
        User Create(string login, UserType userType);

        /// <summary>
        ///     Gibt den Nutzer aus dem Ldap und - wenn vorhanden - mit den Daten aus der DB zurück.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <returns></returns>
        User Get(string login);

        /// <summary>
        ///     Methode zum Aktualisieren eines Nutzers in der DB.
        /// </summary>
        /// <param name="login">Der Login des Nutzers</param>
        /// <param name="userType">Der Typ des Nutzers</param>
        User Update(string login, UserType userType);
    }
}