namespace ThesisManager.Core.Domain.Mappings.Core {
    using ThesisManager.Core.Domain.Core;

    /// <summary>
    ///     Generisches Mapping für ein Objekt mit einer ID.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DomainEntityWithIdMap<T> : DomainEntityMap<T>
        where T : DomainEntityWithId {
        /// <summary>
        ///     Erzeugt ein neues Mapping zu einer <see cref="DomainEntityWithId" />
        /// </summary>
        protected DomainEntityWithIdMap() {
            Id(x => x.Id).Column("ID");
        }
    }
}