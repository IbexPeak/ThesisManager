namespace ThesisManager.Core.Service.Core {
    using System.Collections.Generic;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    /// Schnittstelle für einen Service, wo die Entität eine Id und einen Namen hat.
    /// </summary>
    /// <typeparam name="T">Der Typ der Entität</typeparam>
    public interface IDomainEntityWithIdAndNameService<T> : IDomainEntityWithIdService<T>
            where T : DomainEntityWithIdAndName {
        /// <summary>
        ///   Sucht nach Objekten mit dem übergebenen Namen
        /// </summary>
        /// <param name="name"> Der Name nach dem gesucht werden soll </param>
        /// <returns> Liste mit gefundenen Objekten </returns>
        IList<T> FindByName(string name);

        /// <summary>
        ///   Ändert den Namen eines Domainobjektes
        /// </summary>
        /// <param name="objToUpdate"> Das zu ändernde Objekt </param>
        /// <param name="name"> Der neue Name </param>
        void Update(ref T objToUpdate, string name);

        /// <summary>
        /// Überprüft, ob der Name für ein Objekt verwendet werden kann.
        /// </summary>
        /// <param name="name">Der Name der für das Objekt verwendet und überprüft werden soll.</param>
        /// <param name="validationFor">Das Objekt, für welches der Name verwendet und überprüft werden soll.</param>
        void ValidateName(string name, T validationFor = null);
    }
}