namespace ThesisManager.Core.Persistence {
    using System.Collections.Generic;

    using ThesisManager.Core.Domain;

    /// <summary>
    ///     Schnittstelle für einen Dao, der eine Verbindung zum LDAP hat. Dies wird für die Nutzer benötigt.
    /// </summary>
    public interface ILdapDao {
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
        ///     Aktualisiert einen <see cref="User" /> mit den Daten aus dem Ldap.
        /// </summary>
        /// <param name="user">Der User</param>
        void SetupUser(User user);
    }
}