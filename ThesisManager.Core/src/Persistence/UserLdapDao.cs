namespace ThesisManager.Core.Persistence {
    using System.Collections.Generic;
    using System.Linq;

    using ThesisManager.Core.Domain;

    /// <summary>
    ///     Bildet einen Dao mit Zugriff auf das Ldap ab.
    /// </summary>
    public class UserLdapDao : IUserLdapDao {
        private static readonly IList<User> _users = new List<User>();

        public bool CanLogin(string login, string password) {
            User user = _users.SingleOrDefault(m => Equals(m.Login, login) && Equals(m.Password, password));

            return user != null;
        }

        public void Delete(User user) {
            _users.Remove(user);
        }

        public User Get(string login) {
            User user = _users.SingleOrDefault(m => Equals(m.Login, login));

            return user;
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
    }
}