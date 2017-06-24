namespace ThesisManager.Core.Persistence.Core.FluentHibernate {
    using System.Collections.Generic;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    /// Schnittstelle für einen Fluent-Service, wo die Entität eine Id und einen Namen hat.
    /// </summary>
    /// <typeparam name="T">Der Typ der Entität</typeparam>
    public interface IFluentDomainEntityWithIdAndNameDao<T>:IDomainEntityWithIdAndNameDao<T>
            where T : DomainEntityWithIdAndName {
        /// <summary>
        ///     Sucht in der Persistenzschicht nach Objekten vom Typ T mit diesem Namen.
        /// </summary>
        /// <param name="name"> zu suchender Name </param>
        /// <returns> </returns>
        IList<T> FindByName(string name);

        /// <summary>
        ///     Ruft ab, ob ein Objekt mit diesem Namen existiert.
        /// </summary>
        /// <param name="name"> </param>
        /// <returns> </returns>
        bool IsNameExisting(string name);
    }
}