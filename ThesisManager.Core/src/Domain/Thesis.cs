namespace ThesisManager.Core.Domain {
    using System;
    using System.Collections.Generic;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    /// Bildet eine Abschlussarbeit ab.
    /// </summary>
    public class Thesis : DomainEntityWithId {
        private readonly DateTime? _createdOn;
        private readonly IList<User> _creators;
        private readonly IList<HistoryEntry> _historyEntries = new List<HistoryEntry>();
        private User _firstAdvisor;
        private DateTime? _handoverDate;
        private DateTime? _presentationDate;
        private string _secondAdvisor;
        private ThesisStatus _status;
        private string _topic;

        /// <summary>
        /// Hibernate-Konstruktor
        /// </summary>
        protected Thesis() {
        }

        /// <summary>
        ///     Liefert das Datum der Erstellung.
        /// </summary>
        public DateTime? CreatedOn {
            get { return _createdOn; }
        }

        /// <summary>
        ///     Liefert die Ersteller der Abschlussarbeit.
        /// </summary>
        public IList<User> Creators {
            get { return _creators; }
        }

        /// <summary>
        ///     Liefert oder setzt den Erstbetreuer.
        /// </summary>
        public User FirstAdvisor {
            get { return _firstAdvisor; }
            set { _firstAdvisor = value; }
        }

        /// <summary>
        ///     Liefert oder setzt das Abgabedatum.
        /// </summary>
        public DateTime? HandoverDate {
            get { return _handoverDate; }
            set { _handoverDate = value; }
        }

        /// <summary>
        ///     Liefert die Liste der Historieneinträge.
        /// </summary>
        public IList<HistoryEntry> HistoryEntries {
            get { return _historyEntries; }
        }

        /// <summary>
        ///     Liefert oder setzt das Verteidigungsdatum.
        /// </summary>
        public DateTime? PresentationDate {
            get { return _presentationDate; }
            set { _presentationDate = value; }
        }

        /// <summary>
        ///     Liefert oder setzt den Zweitbetreuer.
        /// </summary>
        public string SecondAdvisor {
            get { return _secondAdvisor; }
            set { _secondAdvisor = value; }
        }

        /// <summary>
        ///     Liefert oder setzt den Status der Abschlussarbeit.
        /// </summary>
        public ThesisStatus Status {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        ///     Liefert oder setzt das Thema der Abschlussarbeit.
        /// </summary>
        public string Topic {
            get { return _topic; }
            set { _topic = value; }
        }
    }
}