namespace ThesisManager.Core.Service {
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            get { return UserDbDao; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        ///     Liefert oder setzt den <see cref="IUserDbDao" />. Wird von Spring gesetzt.
        /// </summary>
        protected IUserDbDao UserDbDao { get; set; }

        /// <summary>
        ///     Liefert oder setzt den <see cref="IUserLdapDao" />. Wird von Spring gesetzt.
        /// </summary>
        protected IUserLdapDao UserLdapDao { get; set; }

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
            return UserLdapDao.CanLogin(login, password);
        }

        public override IList<User> GetAll() {
            IList<User> db = UserDbDao.GetAll();
            IList<User> ldap = UserLdapDao.GetAll();

            IList<User> result = new List<User>();

            foreach (User ldapUser in ldap) {
                User dbUser = db.SingleOrDefault(m => Equals(m.Login, ldapUser.Login));
                if (dbUser != null) {
                    ldapUser.UpdateUser(dbUser.UserType);
                    result.Add(ldapUser);
                }
            }
            return result;
        }

        /// <summary>
        ///     Methode zum Erstellen eines Nutzers.
        /// </summary>
        /// <param name="login">Der Login des Nutzers</param>
        /// <param name="userType">Der Typ des Nutzers</param>
        public User Create(string login, UserType userType) {
            if (login == null) {
                throw new ArgumentNullException(nameof(login));
            }
            if (!UserLdapDao.IsUserExisting(login)) {
                throw new InvalidOperationException();
            }
            User user = UserLdapDao.Get(login);

            user.UpdateUser(userType);

            UserDbDao.Save(user);

            return user;
        }

        /// <summary>
        ///     Gibt den Nutzer aus der DB mit den Daten aus dem LDAP zurück.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <returns></returns>
        public User Get(string login) {

            User ldapUser = UserLdapDao.Get(login);
            User dbUser = UserDbDao.GetUser(login);

            ldapUser.UpdateUser(dbUser.UserType);

            return ldapUser;
        }

        /// <summary>
        ///     Methode zum Aktualisieren eines Nutzers.
        /// </summary>
        /// <param name="login">Der Login des Nutzers</param>
        /// <param name="userType">Der Typ des Nutzers</param>
        public User Update(string login, UserType userType) {
            if (login == null) {
                throw new ArgumentNullException(nameof(login));
            }
            User user = UserDbDao.GetUser(login);

            user.UpdateUser(userType);

            UserDbDao.Save(user);

            return user;
        }
    }
}