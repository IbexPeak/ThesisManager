namespace ThesisManager.Core.Domain.Mappings {
    using ThesisManager.Core.Domain.Mappings.Core;

    /// <summary>
    ///     Bildet das Mapping für einen <see cref="HistoryEntry" /> ab.
    /// </summary>
    public class HistoryEntryMap : DomainEntityWithIdMap<HistoryEntry> {
        protected HistoryEntryMap() {
            References(m => m.ChangedBy);
            Map(m => m.Date);
            Map(m => m.Info);
            Map(m => m.OldValue);
            Map(m => m.Type).CustomType<HistoryEntryType>();
        }
    }
}