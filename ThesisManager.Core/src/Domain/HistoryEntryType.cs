namespace ThesisManager.Core.Domain {
    /// <summary>
    ///     Listet die Status ab, auf die sich ein Historieneintrag beziehen kann.
    /// </summary>
    public enum HistoryEntryType {
        /// <summary>
        ///     Erstbetreuer
        /// </summary>
        FirstAdvisor,

        /// <summary>
        ///     Präsentationsdatum
        /// </summary>
        PresentationDate,

        /// <summary>
        ///     Abgabedatum
        /// </summary>
        HandoverDate,

        /// <summary>
        ///     Zweitbetreuer
        /// </summary>
        SecondAdvisor,

        /// <summary>
        ///     Thema
        /// </summary>
        Topic,

        /// <summary>
        ///     Status
        /// </summary>
        Status
    }
}