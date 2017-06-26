namespace ThesisManager.Core.Persistence {
    using System.Collections.Generic;

    using ThesisManager.Core.Domain;

    /// <summary>
    ///     Schnittstelle für einen Dao, der eine Verbindung zum LDAP hat. Dies wird für die <see cref="User"/> benötigt.
    /// </summary>
    public interface IUserLdapDao {
        /// <summary>
        ///     Prüft ob ein Nutzer eingeloggt werden kann.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <param name="password">Das Password</param>
        /// <returns></returns>
        bool CanLogin(string login, string password);

        /// <summary>
        ///     Löscht einen Nutzer aus dem LDAP. Nur für TestDaten.
        /// </summary>
        /// <param name="user"></param>
        void Delete(User user);

        /// <summary>
        ///     Liefert alle Nutzer aus dem Ldap.
        /// </summary>
        IList<User> GetAll();

        /// <summary>
        ///     Gibt an, ob ein <see cref="User" /> mit dem Login im Ldap existiert.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <returns></returns>
        bool IsUserExisting(string login);

        /// <summary>
        ///     Speichert einen Nutzer im Ldap. Nur für Testdaten.
        /// </summary>
        /// <param name="user"></param>
        void Save(User user);

        /// <summary>
        /// Liefert einen Nutzer aus dem Ldap.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <returns></returns>
        User Get(string login);
    }
}