namespace ThesisManager.Core.Domain {
    using System;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    ///     Bildet einen Historieneintrag zur Abschlussarbeit ab.
    /// </summary>
    public class HistoryEntry : DomainEntityWithId {
        private DateTime? _date;
        private string _info;
        private string _oldValue;
        private HistoryEntryType _type;
        private User _changedBy;

        /// <summary>
        ///     Liefert oder setzt den Nutzer, der den Eintrag geändert hat.
        /// </summary>
        public User ChangedBy {
            get { return _changedBy; }
            set { _changedBy = value; }
        }

        /// <summary>
        ///     Liefert oder setzt das Historiendatum.
        /// </summary>
        public DateTime? Date {
            get { return _date; }
            set { _date = value; }
        }

        /// <summary>
        ///     Liefert oder setzt die Bemerkung zum Historieneintrag.
        /// </summary>
        public string Info {
            get { return _info; }
            set { _info = value; }
        }

        /// <summary>
        ///     Liefert oder setzt den alten Wert des Historieneintrags.
        /// </summary>
        public string OldValue {
            get { return _oldValue; }
            set { _oldValue = value; }
        }

        /// <summary>
        ///     Liefert oder setzt den Typ des Historieneintrags.
        /// </summary>
        public HistoryEntryType Type {
            get { return _type; }
            set { _type = value; }
        }
    }
}