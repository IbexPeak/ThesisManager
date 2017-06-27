namespace ThesisManager.Core.Domain {
    using System;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    ///     Bildet einen Credit-Eintrag zu einem Nutzer und einer Abschlussarbeit ab.
    /// </summary>
    public class Credit : DomainEntityWithId {
        private readonly int _amount;
        private readonly string _subject;
        private readonly Thesis _thesis;
        private readonly User _user;

        /// <summary>
        ///     Erstellt ein neuen Credit.
        /// </summary>
        /// <param name="user">Der Nutzer</param>
        /// <param name="thesis">Die Abschlussarbeit</param>
        /// <param name="subject">Das Fach</param>
        /// <param name="amount">Die Anzahl der Credits</param>
        public Credit(User user, Thesis thesis, string subject, int amount) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }
            if (thesis == null) {
                throw new ArgumentNullException(nameof(thesis));
            }
            if (subject == null) {
                throw new ArgumentNullException(nameof(subject));
            }
            _user = user;
            _thesis = thesis;
            _subject = subject;
            _amount = amount;
        }

        /// <summary>
        ///     Hibernate-Konstruktor
        /// </summary>
        protected Credit() {
        }

        /// <summary>
        ///     Liefert die Anzahl der Credits auf das Fach.
        /// </summary>
        public int Amount {
            get { return _amount; }
        }

        /// <summary>
        ///     Liefert das Fach.
        /// </summary>
        public string Subject {
            get { return _subject; }
        }

        /// <summary>
        ///     Liefert die Abschlussarbeit.
        /// </summary>
        public Thesis Thesis {
            get { return _thesis; }
        }

        /// <summary>
        ///     Liefert den Nutzer.
        /// </summary>
        public User User {
            get { return _user; }
        }
    }
}