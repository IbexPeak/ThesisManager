namespace ThesisManager.Core.Persistence.Core {
    using NHibernate;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    /// Generische Basisklasse für alle DAOs, die sich um DomainEntitiesWithId kümmern.
    /// T muss vom Typ <see cref="DomainEntityWithId"/> sein.
    /// </summary>
    /// <typeparam name="T">Typ der durch die Klasse behandelt wird.</typeparam>
    public abstract class DomainEntityWithIdDao<T> : DomainEntityDao<T>, IDomainEntityWithIdDao<T> where T : DomainEntityWithId {
        /// <summary>
        /// Sucht ein Objekt vom Typ T mit der übergebenen Id.
        /// </summary>
        /// <param name="id">die zu suchende Objekt ID</param>
        /// <exception cref="ObjectNotFoundException">Wenn kein Objekt mit der entsprechenden ID gefunden wird.</exception>
        /// <returns>Objekt mit der gesuchten ID</returns>
        public virtual T Get(int id) {
            T foundObject = HibernateTemplate.Get<T>(id);
            if (foundObject == null){
                throw new ObjectNotFoundException(id, typeof(T));
            }
            return foundObject;
        }
    }
}