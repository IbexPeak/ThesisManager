namespace ThesisManager.Core.Persistence {
    using NHibernate;
    using NHibernate.Criterion;

    using ThesisManager.Core.Domain;
    using ThesisManager.Core.Persistence.Core;
    using ThesisManager.Core.Persistence.Core.FluentHibernate;

    /// <summary>
    ///     Ein Dao für die Verwaltung von <see cref="User" />.
    /// </summary>
    public class UserDao : DomainEntityWithIdDao<User>, IUserDao {
        /// <summary>
        ///     Liefert einen <see cref="User" /> anhand des Logins.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <returns></returns>
        public User GetUser(string login) {
            ICriteria criteria = Session.CreateCriteria<User>();
            criteria.Add(Restrictions.Eq(Member.ToString((User u) => u.Login), login));
            return criteria.UniqueResult<User>();
        }
    }
}