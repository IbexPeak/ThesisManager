namespace ThesisManager.Core.Persistence.Core {
    using System.Collections.Generic;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    /// Schnittstelle für einen Dao, wo die Entität eine Id und einen Namen hat.
    /// </summary>
    /// <typeparam name="T">Der Typ der Entität</typeparam>
    public interface IDomainEntityWithIdAndNameDao<T> : IDomainEntityWithIdDao<T> where T: DomainEntityWithIdAndName {
        /// <summary>
        /// Sucht in der Persistenzschicht nach Objekten vom Typ T mit diesem Namen.
        /// </summary>
        /// <param name="name">zu suchender Name</param>
        /// <returns></returns>
        IList<T> FindByName(string name);

        /// <summary>
        ///   Sucht in der Persistenzschicht nach Objekten vom Typ T mit diesem Namen außer einem Objekt, das nicht berücksichtigt werden soll.
        /// </summary>
        /// <param name="name"> zu suchender Name </param>
        /// <param name="except">Objekt, dass nicht berücksichtigt werden soll. Bei Null werden alle vorhandenen Objekte berücksichtigt.</param>
        /// <returns> </returns>
        IList<T> FindByNameExcept(string name, T except);

        /// <summary>
        /// Ruft ab, ob ein Objekt mit diesem Namen existiert.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsNameExisting(string name);

    }
}