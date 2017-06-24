namespace ThesisManager.Core.Domain.Mappings.Core {
    using FluentNHibernate.Mapping;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    ///     Generisches Mapping für ein Objekt mit einer BusinessId.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DomainEntityMap<T> : ClassMap<T>
        where T : DomainEntity {
        /// <summary>
        ///     Erzeugt ein neues Mapping zu einer <see cref="DomainEntity" />
        /// </summary>
        protected DomainEntityMap() {
            Map(m => m.BusinessId);
        }
    }
}