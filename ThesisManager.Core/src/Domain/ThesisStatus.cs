namespace ThesisManager.Core.Domain {
    /// <summary>
    /// Bildet den Status der Abschlussarbeit ab.
    /// </summary>
    public enum ThesisStatus {
        /// <summary>
        /// Erstellt
        /// </summary>
        Created,

        /// <summary>
        /// Erstellung wiederrufen
        /// </summary>
        CreationRevoked,

        /// <summary>
        /// Verlängert
        /// </summary>
        Elongated,

        /// <summary>
        /// Präsentation abgeschlossen
        /// </summary>
        Presented
    }
}