namespace ThesisManager.Core.Service.Core {
    using System;
    using System.Collections.Generic;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    ///     Schnittstelle für einen Service, wo die Entität keine Id hat.
    /// </summary>
    /// <typeparam name="T">Der Typ der Entität</typeparam>
    public interface IDomainEntityService<T>
        where T : DomainEntity {
        /// <summary>
        ///     Löscht das übergebene Objekt wenn möglich aus der Persistence-Schicht (z.B. Datenbank).
        /// </summary>
        /// <param name="itemToBeDeleted"> </param>
        void Delete(T itemToBeDeleted);

        /// <summary>
        ///     Flushes and Cleares Session of Dao. For testing purposes
        /// </summary>
        void FlushAndClear();

        /// <summary>
        ///     Ruft eine Liste mit allen in der Persistence-Schicht hinterlegten Objekten dieses Typs ab.
        /// </summary>
        /// <returns> </returns>
        IList<T> GetAll();

        /// <summary>
        ///     Ruft das Objekt mit der übergebenen BusinessId aus der Datenbank ab. Existiert kein Objekt mit der entsprechenden
        ///     BusinessId wird eine Exception geworfen.
        /// </summary>
        /// <param name="guid"> </param>
        /// <returns> </returns>
        T GetByBusinessId(Guid guid);

        /// <summary>
        ///     Ruft die Anzahl aller in der Persistence-Schicht hinterlegten Objekte dieses Typs ab.
        /// </summary>
        int GetCount();

        /// <summary>
        ///     Testet ob das übergebene Objekt referenziert ist und dadurch zum Beispiel nicht gelöscht werden darf.
        /// </summary>
        /// <param name="objectToTest"> Das zu testende Objekt. </param>
        /// <returns> True wenn das Objekt referenziert ist oder false wenn nicht. </returns>
        bool IsReferenced(T objectToTest);
    }
}