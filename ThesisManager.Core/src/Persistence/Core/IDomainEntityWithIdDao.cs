namespace ThesisManager.Core.Persistence.Core {
    using NHibernate;

    /// <summary>
    /// Interface für die allgemeinen Funktionen zur Verwaltung von Entitäten
    /// auf der Persistenzebene.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDomainEntityWithIdDao<T> : IDomainEntityDao<T> {

        /// <summary>
        /// Sucht ein Objekt vom Typ T mit der übergebenen Id.
        /// </summary>
        /// <param name="id">die zu suchende Objekt ID</param>
        /// <exception cref="ObjectNotFoundException">Wenn kein Objekt mit der entsprechenden ID gefunden wird.</exception>
        /// <returns>Objekt mit der gesuchten ID</returns>
        T Get(int id);
    }
}