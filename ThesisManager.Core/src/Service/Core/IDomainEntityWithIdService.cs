namespace ThesisManager.Core.Service.Core {
    using NHibernate;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    /// Schnittstelle für einen Service, wo die Entität eine Id hat.
    /// </summary>
    /// <typeparam name="T">Der Typ der Entität</typeparam>
    public interface IDomainEntityWithIdService<T> : IDomainEntityService<T> where T : DomainEntityWithId {

        /// <summary>
        /// Lädt ein Item anhand seiner Id.
        /// Wird kein Item gefunden wird eine Exception geworfen.
        /// </summary>
        /// <param name="id">Die Id nach der gesucht werden soll.</param>
        /// <returns>Das Objekt mit dieser Id</returns>
        /// <exception cref="ObjectNotFoundException"></exception>
        T GetById(int id);
    }
}