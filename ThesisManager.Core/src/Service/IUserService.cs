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
        ///     Methode zum Erstellen eines Nutzers.
        /// </summary>
        /// <param name="login">Der Login des Nutzers</param>
        /// <param name="userType">Der Typ des Nutzers</param>
        /// <param name="userPermissions">Die Liste der Berechtigungen des Nutzers</param>
        User Create(string login, UserType userType, IList<UserPermission> userPermissions);

        /// <summary>
        ///     Gibt den Nutzer aus der DB mit den Daten aus dem LDAP zurück.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <returns></returns>
        User Get(string login);

        /// <summary>
        ///     Methode zum Aktualisieren eines Nutzers.
        /// </summary>
        /// <param name="login">Der Login des Nutzers</param>
        /// <param name="userType">Der Typ des Nutzers</param>
        /// <param name="userPermissions">Die Liste der Berechtigungen des Nutzers</param>
        User Update(string login, UserType userType, IList<UserPermission> userPermissions);
    }
}