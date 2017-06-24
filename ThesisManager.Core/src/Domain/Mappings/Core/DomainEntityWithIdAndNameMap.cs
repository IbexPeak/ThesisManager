namespace ThesisManager.Core.Domain.Mappings.Core {
    using FluentNHibernate.Mapping;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    ///     Basismapping für eine Klasse mit ID und Name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DomainEntityWithIdAndNameMap<T> : DomainEntityWithIdMap<T>
        where T : DomainEntityWithIdAndName {
        /// <summary>
        ///     Erstellt eine neue Instanz der Mapping Klasse.
        ///     Optional besteht die Möglichkeit anzugeben ob der Name der Entität einmalig sein soll.
        /// </summary>
        /// <param name="isNameUnique"></param>
        protected DomainEntityWithIdAndNameMap(bool isNameUnique = false) {
            PropertyPart nameMap = Map(x => x.Name);
            if (isNameUnique) {
                nameMap.Unique();
            }
        }
    }
}