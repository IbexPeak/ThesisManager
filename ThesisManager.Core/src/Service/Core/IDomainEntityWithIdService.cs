namespace ThesisManager.Core.Service.Core {
    using NHibernate;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    /// Schnittstelle f�r einen Service, wo die Entit�t eine Id hat.
    /// </summary>
    /// <typeparam name="T">Der Typ der Entit�t</typeparam>
    public interface IDomainEntityWithIdService<T> : IDomainEntityService<T> where T : DomainEntityWithId {

        /// <summary>
        /// L�dt ein Item anhand seiner Id.
        /// Wird kein Item gefunden wird eine Exception geworfen.
        /// </summary>
        /// <param name="id">Die Id nach der gesucht werden soll.</param>
        /// <returns>Das Objekt mit dieser Id</returns>
        /// <exception cref="ObjectNotFoundException"></exception>
        T GetById(int id);
    }
}