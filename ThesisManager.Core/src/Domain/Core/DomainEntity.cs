namespace ThesisManager.Core.Domain.Core {
    using System;

    /// <summary>
    ///   Basisklasse für alle Domainobjekte.
    /// </summary>
    public abstract class DomainEntity : IEquatable<object> {
        private readonly Guid _businessId;

        /// <summary>
        ///   Standardkonstruktor
        /// </summary>
        protected DomainEntity() {
            _businessId = Guid.NewGuid();
        }

        /// <summary>
        ///   Ruft eine eindeutige Id zur Identifikation des Objektes ab.
        /// </summary>
        public virtual Guid BusinessId {
            get { return _businessId; }
        }

        /// <summary>
        /// Bestimmt, ob das angegebene <see cref="T:System.Object"/> und das aktuelle <see cref="T:System.Object"/> gleich sind.
        /// </summary>
        /// <returns>
        /// true, wenn das angegebene <see cref="T:System.Object"/> gleich dem aktuellen <see cref="T:System.Object"/> ist, andernfalls false.
        /// </returns>
        /// <param name="obj">Das <see cref="T:System.Object"/>, das mit dem aktuellen <see cref="T:System.Object"/> verglichen werden soll. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj) {
            DomainEntity testObj = obj as DomainEntity;

            if (testObj == null) {
                return false;
            }

            return testObj.BusinessId.Equals(BusinessId);
        }

        /// <summary>
        /// Fungiert als Hashfunktion für einen bestimmten Typ. 
        /// </summary>
        /// <returns>
        /// Ein Hashcode für das aktuelle <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode() {
            return GetType().GetHashCode() ^ BusinessId.GetHashCode();
        }
    }
}