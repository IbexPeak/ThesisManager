namespace ThesisManager.Core.Domain {
    /// <summary>
    ///     Bildet die Nutzertypen ab.
    /// </summary>
    public enum UserType {
        /// <summary>
        ///     Ein Student.
        /// </summary>
        Student,

        /// <summary>
        ///     Ein Mitarbeiter des Sekretariats.
        /// </summary>
        Secretary,

        /// <summary>
        ///     Ein Mitarbeiter des Prüfungssausschusses.
        /// </summary>
        AuditCommittee,

        /// <summary>
        ///     Ein Professor der HTW Dresden.
        /// </summary>
        Professor
    }
}