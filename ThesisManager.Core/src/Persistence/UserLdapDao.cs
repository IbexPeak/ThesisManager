namespace ThesisManager.Core.Persistence {
    using System.Collections.Generic;
    using System.Linq;

    using NHibernate;

    using ThesisManager.Core.Domain;

    /// <summary>
    /// Bildet einen Dao mit Zugriff auf das Ldap ab.
    /// </summary>
    public class UserLdapDao : IUserLdapDao {
        private readonly IList<User> _users = new List<User>();

        public bool CanLogin(string login, string password) {
            User user = _users.SingleOrDefault(m => Equals(m.Login, login) && Equals(m.Password, password));

            return user != null;
        }

        public void Delete(User user) {
            _users.Remove(user);
        }

        public IList<User> GetAll() {
            return _users;
        }

        public bool IsUserExisting(string login) {
            User user = _users.SingleOrDefault(m => Equals(m.Login, login));

            return user != null;
        }

        public void Save(User user) {
            _users.Add(user);
        }

        public void SetupUser(User user) {
            User ldapUser = _users.SingleOrDefault(m => Equals(m.Login, user.Login));

            if (ldapUser == null) {
                throw new ObjectNotFoundException(user, typeof(User));
            }

            user.SetupUser(ldapUser.Firstname,
                           ldapUser.Lastname,
                           ldapUser.Street,
                           ldapUser.Housenumber,
                           ldapUser.Zipcode,
                           ldapUser.City,
                           ldapUser.Email,
                           ldapUser.Phone,
                           ldapUser.Birthday);
        }
    }
}