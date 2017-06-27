namespace ThesisManager.Core.Persistence {
    using System.Collections.Generic;
    using System.Linq;

    using ThesisManager.Core.Domain;

    /// <summary>
    ///     Bildet einen Dao mit Zugriff auf das Ldap ab.
    /// </summary>
    public class UserLdapDao : IUserLdapDao {
        private static readonly IList<User> _users = new List<User>();

        /// <summary>
        ///     Prüft ob ein Nutzer eingeloggt werden kann.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <param name="password">Das Password</param>
        /// <returns></returns>
        public bool CanLogin(string login, string password) {
            User user = _users.SingleOrDefault(m => Equals(m.Login, login) && Equals(m.Password, password));

            return user != null;
        }

        /// <summary>
        ///     Löscht einen Nutzer aus dem LDAP. Nur für TestDaten.
        /// </summary>
        /// <param name="user"></param>
        public void Delete(User user) {
            _users.Remove(user);
        }

        /// <summary>
        /// Liefert einen Nutzer aus dem Ldap.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <returns></returns>
        public User Get(string login) {
            User user = _users.SingleOrDefault(m => Equals(m.Login, login));

            return user;
        }

        /// <summary>
        ///     Liefert alle Nutzer aus dem Ldap.
        /// </summary>
        public IList<User> GetAll() {
            return _users;
        }

        /// <summary>
        ///     Gibt an, ob ein <see cref="User" /> mit dem Login im Ldap existiert.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <returns></returns>
        public bool IsUserExisting(string login) {
            User user = _users.SingleOrDefault(m => Equals(m.Login, login));

            return user != null;
        }

        /// <summary>
        ///     Speichert einen Nutzer im Ldap. Nur für Testdaten.
        /// </summary>
        /// <param name="user"></param>
        public void Save(User user) {
            _users.Add(user);
        }
    }
}