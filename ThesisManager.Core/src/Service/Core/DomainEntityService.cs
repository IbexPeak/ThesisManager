namespace ThesisManager.Core.Service.Core {
    using System;
    using System.Collections.Generic;

    using Common.Logging;

    using Spring.Transaction.Interceptor;

    using ThesisManager.Core.Domain.Core;
    using ThesisManager.Core.Exceptions;
    using ThesisManager.Core.Persistence.Core;

    /// <summary>
    ///     Klasse f�r einen Service, wo die Entit�t keine Id hat.
    /// </summary>
    /// <typeparam name="T">Der Typ der Entit�t</typeparam>
    public abstract class DomainEntityService<T> : IDomainEntityService<T>
        where T : DomainEntity {
        protected ILog _logger;

        /// <summary>
        ///     Default constructor creates a new logger object for the current concrete type.
        /// </summary>
        protected DomainEntityService() {
            _logger = LogManager.GetLogger(GetType());
        }

        /// <summary>
        ///     Ruft das Data-Access-Object ab, welches f�r den Zugriff auf die Persistence-Schicht verwendet wird ab.
        /// </summary>
        protected abstract IDomainEntityDao<T> Dao { get; set; }

        /// <summary>
        ///     L�scht das �bergebene Objekt wenn m�glich aus der Persistence-Schicht (z.B. Datenbank).
        /// </summary>
        /// <param name="itemToBeDeleted"> </param>
        [Transaction]
        public virtual void Delete(T itemToBeDeleted) {
            if (itemToBeDeleted == null) {
                throw new ArgumentNullException("itemToBeDeleted");
            }

            if (Dao.IsReferenced(itemToBeDeleted)) {
                throw new ObjectReferencedException("Der Eintrag kann nicht gel�scht werden, da er referenziert ist.",
                                                    itemToBeDeleted.ToString());
            }

            Dao.Delete(itemToBeDeleted);
        }

        /// <summary>
        ///     Flushes and Cleares Session of Dao. For testing purposes
        /// </summary>
        public void FlushAndClear() {
            Dao.FlushAndClear();
        }

        /// <summary>
        ///     Ruft eine Liste mit allen in der Persistence-Schicht hinterlegten Objekten dieses Typs ab.
        /// </summary>
        /// <returns> </returns>
        [Transaction]
        public virtual IList<T> GetAll() {
            return Dao.GetAll();
        }

        /// <summary>
        ///     Ruft das Objekt mit der �bergebenen BusinessId aus der Datenbank ab. Existiert kein Objekt mit der entsprechenden
        ///     BusinessId wird eine Exception geworfen.
        /// </summary>
        /// <param name="guid"> </param>
        /// <returns> </returns>
        [Transaction]
        public virtual T GetByBusinessId(Guid guid) {
            return Dao.GetByBusinessId(guid);
        }

        /// <summary>
        ///     Ruft die Anzahl aller in der Persistence-Schicht hinterlegten Objekte dieses Typs ab.
        /// </summary>
        [Transaction]
        public virtual int GetCount() {
            return Dao.GetCount();
        }

        /// <summary>
        ///     Testet ob das �bergebene Objekt referenziert ist und dadurch zum Beispiel nicht gel�scht werden darf.
        /// </summary>
        /// <param name="objectToTest"> Das zu testende Objekt. </param>
        /// <returns> True wenn das Objekt referenziert ist oder false wenn nicht. </returns>
        [Transaction]
        public virtual bool IsReferenced(T objectToTest) {
            return Dao.IsReferenced(objectToTest);
        }
    }
}