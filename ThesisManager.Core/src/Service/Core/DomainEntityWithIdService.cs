namespace ThesisManager.Core.Service.Core {
    using NHibernate;

    using Spring.Transaction.Interceptor;

    using ThesisManager.Core.Domain.Core;
    using ThesisManager.Core.Persistence.Core;

    /// <summary>
    /// Klasse f�r einen Service, wo die Entit�t eine Id hat.
    /// </summary>
    /// <typeparam name="T">Der Typ der Entit�t</typeparam>
    public abstract class DomainEntityWithIdService<T> : DomainEntityService<T>, IDomainEntityWithIdService<T>
            where T : DomainEntityWithId {

        /// <summary>
        ///   Ruft das Data-Access-Object ab, welches f�r den Zugriff auf die Persistence-Schicht verwendet wird ab.
        /// </summary>
        protected abstract IDomainEntityWithIdDao<T> IdDao { get; set; }

        /// <summary>
        ///   Ruft das Data-Access-Object ab, welches f�r den Zugriff auf die Persistence-Schicht verwendet wird ab.
        /// </summary>
        protected override IDomainEntityDao<T> Dao {
            get { return IdDao; }
            set { IdDao = (IDomainEntityWithIdDao<T>)value; }
        }

        /// <summary>
        ///   L�dt ein Item anhand seiner Id. Wird kein Item gefunden wird eine Exception geworfen.
        /// </summary>
        /// <param name="id"> Die Id nach der gesucht werden soll. </param>
        /// <returns> Das Objekt mit dieser Id </returns>
        /// <exception cref="ObjectNotFoundException"></exception>
        [Transaction]
        public virtual T GetById(int id) {
            return IdDao.Get(id);
        }
    }
}