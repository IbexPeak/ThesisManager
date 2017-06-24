namespace ThesisManager.Core.Persistence.Core {
    using System;
    using System.Collections.Generic;

    using NHibernate.Criterion;

    /// <summary>
    /// Interface für die allgemeinen Funktionen zur Verwaltung von Entitäten
    /// auf der Persistenzebene.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDomainEntityDao<T> {
        /// <summary>
        /// Liefert eine Liste mit allen Objekten
        /// vom Typ T.
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary>
        /// Liefert eine Liste mit allen in der Datenbank gespeicherten Objekten vom Typ T sortiert nach den übergebenen Kriterien.
        /// </summary>
        /// <param name="orders">Sortierkriterien oder null wenn keine Sortierung gewünscht ist.</param>
        /// <returns></returns>
        IList<T> GetAll(IEnumerable<Order> orders);

        /// <summary>
        /// Liefert eine Liste mit allen in der Datenbank gefundenen Objekten vom Typ T für welche die Restriktionen zutreffen.
        /// </summary>
        /// <param name="restrictions">Restriktionen zur Einschränkung</param>
        /// <param name="orders">Sortierung</param>
        /// <returns></returns>
        IList<T> Find(IEnumerable<ICriterion> restrictions, IEnumerable<Order> orders);


        /// <summary>
        /// Liefert die Anzahl der gefunden Objekte.
        /// </summary>
        /// <returns></returns>
        int GetCount();

        /// <summary>
        /// Speichert ein Objekt vom Typ T.
        /// </summary>
        /// <param name="value">zu speicherndes Objekt.</param>
        void Save(T value);

        /// <summary>
        /// Löscht das Objekt vom Typ T.
        /// </summary>
        /// <param name="value">zu löschendes Objekt.</param>
        void Delete(T value);

        /// <summary>
        /// Schreibt die aktuelle Session in die Datenbank und leert sie anschließend.
        /// </summary>          
        void FlushAndClear();

        /// <summary>
        /// Überprüft ob ein Objekt in der Datenbank referenziert ist.
        /// </summary>
        /// <param name="objectToTest">Das zu testende Objekt.</param>
        /// <returns>true - wenn das Objekt referenziert ist
        /// false - wenn keine Referenz auf das Objekt zeigt</returns>
        bool IsReferenced(T objectToTest);

        /// <summary>
        /// Ruft das Objekt mit der übergebenen BusinessId aus der Datenbank ab.
        /// Existiert kein Objekt mit der entsprechenden BusinessId wird eine Exception geworfen.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        T GetByBusinessId(Guid guid);
    }
}