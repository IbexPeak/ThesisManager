namespace ThesisManager.Core.Domain.Mappings {
    using ThesisManager.Core.Domain.Mappings.Core;

    /// <summary>
    ///     Bildet das Mapping für einen <see cref="Credit" /> ab.
    /// </summary>
    public class CreditMap : DomainEntityWithIdMap<Credit> {
        protected CreditMap() {
            Map(m => m.Amount);
            Map(m => m.Subject);
            References(m => m.User);
            References(m => m.Thesis);
        }
    }
}