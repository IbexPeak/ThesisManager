namespace ThesisManager.Core.Domain.Mappings {
    using ThesisManager.Core.Domain.Mappings.Core;

    /// <summary>
    ///     Bildet das Mapping für einen <see cref="User" /> ab.
    /// </summary>
    public class UserMap : DomainEntityWithIdMap<User> {
        /// <summary>
        ///     Konstruktor für das Mapping.
        /// </summary>
        protected UserMap() {
            Map(m => m.Login);
            Map(m => m.UserType).CustomType<UserType>();
        }
    }
}