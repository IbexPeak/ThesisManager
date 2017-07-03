namespace ThesisManager.Core.Domain.Mappings {
    using ThesisManager.Core.Domain.Mappings.Core;

    /// <summary>
    ///     Bildet das Mapping für eine <see cref="Thesis" /> ab.
    /// </summary>
    public class ThesisMap : DomainEntityWithIdMap<Thesis> {
        protected ThesisMap() {
            Map(m => m.CreatedOn);
            HasManyToMany(x => x.Creators).Cascade.All().Table("tblThesisToUser");
            References(m => m.FirstAdvisor);
            Map(m => m.HandoverDate);
            Map(m => m.PresentationDate);
            Map(m => m.SecondAdvisor);
            Map(m => m.Topic);
            Map(m => m.Status).CustomType<ThesisStatus>();
            HasMany(m => m.HistoryEntries);
        }
    }
}