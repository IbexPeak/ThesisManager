namespace ThesisManager.Core.Domain {
    /// <summary>
    ///     Bildet die Berechtigungen eines Nutzers ab.
    /// </summary>
    public enum UserPermission {
        /// <summary>
        ///     Kann die Nutzerliste einsehen.
        /// </summary>
        CanViewUsers,

        /// <summary>
        ///     Kann Nutzer verwalten.
        /// </summary>
        CanManageUsers
    }
}