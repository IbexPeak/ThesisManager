﻿namespace ThesisManager.Core.Persistence {
    using ThesisManager.Core.Domain;
    using ThesisManager.Core.Persistence.Core;

    /// <summary>
    ///     Schnittstelle für den Dao für die die Verwaltung von <see cref="User" />.
    /// </summary>
    public interface IUserDao : IDomainEntityWithIdDao<User> {
        /// <summary>
        ///     Liefert einen <see cref="User" /> anhand des Logins.
        /// </summary>
        /// <param name="login">Der Login</param>
        /// <returns></returns>
        User GetUser(string login);
    }
}