namespace ThesisManager.Core.Service {
    using System;
    using System.Collections.Generic;

    using ThesisManager.Core.Domain;
    using ThesisManager.Core.Persistence;
    using ThesisManager.Core.Persistence.Core;
    using ThesisManager.Core.Service.Core;

    /// <summary>
    ///     EIn Service zur Verwaltung von <see cref="User" />.
    /// </summary>
    public class UserService : DomainEntityWithIdService<User>, IUserService {
        /// <summary>
        ///     Ruft das Data-Access-Object ab, welches für den Zugriff auf die Persistence-Schicht verwendet wird.
        /// </summary>
        protected override IDomainEntityWithIdDao<User> IdDao {
            get { return UserDao; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        ///     Liefert oder setzt den <see cref="ILdapDao" />. Wird von Spring gesetzt.
        /// </summary>
        protected ILdapDao LdapDao { get; set; }

        /// <summary>
        ///     Liefert oder setzt den <see cref="IUserDao" />. Wird von Spring gesetzt.
        /// </summary>
        protected IUserDao UserDao { get; set; }

        /// <summary>
        ///     Prüft ob ein Nutzer eingeloggt werden kann.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <param name="password">Das Password</param>
        /// <returns></returns>
        public bool CanLogin(string login, string password) {
            if (login == null) {
                throw new ArgumentNullException(nameof(login));
            }
            if (password == null) {
                throw new ArgumentNullException(nameof(password));
            }
            return LdapDao.CanLogin(login, password);
        }

        /// <summary>
        ///     Methode zum Erstellen eines Nutzers.
        /// </summary>
        /// <param name="login">Der Login des Nutzers</param>
        /// <param name="userType">Der Typ des Nutzers</param>
        /// <param name="userPermissions">Die Liste der Berechtigungen des Nutzers</param>
        public User Create(string login, UserType userType, IList<UserPermission> userPermissions) {
            if (login == null) {
                throw new ArgumentNullException(nameof(login));
            }
            if (userPermissions == null) {
                throw new ArgumentNullException(nameof(userPermissions));
            }
            if (!LdapDao.IsUserExisting(login)) {
                throw new InvalidOperationException();
            }
            User user = new User(login, userType, userPermissions);

            UserDao.Save(user);

            LdapDao.SetupUser(user);

            return user;
        }

        /// <summary>
        ///     Gibt den Nutzer aus der DB mit den Daten aus dem LDAP zurück.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <returns></returns>
        public User Get(string login) {
            User user = UserDao.GetUser(login);

            LdapDao.SetupUser(user);

            return user;
        }

        /// <summary>
        ///     Methode zum Aktualisieren eines Nutzers.
        /// </summary>
        /// <param name="login">Der Login des Nutzers</param>
        /// <param name="userType">Der Typ des Nutzers</param>
        /// <param name="userPermissions">Die Liste der Berechtigungen des Nutzers</param>
        public User Update(string login, UserType userType, IList<UserPermission> userPermissions) {
            if (login == null) {
                throw new ArgumentNullException(nameof(login));
            }
            if (userPermissions == null) {
                throw new ArgumentNullException(nameof(userPermissions));
            }
            User user = UserDao.GetUser(login);

            user.UpdateUser(userType, userPermissions);

            UserDao.Save(user);

            return user;
        }
    }
}