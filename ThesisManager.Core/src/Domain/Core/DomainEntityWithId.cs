namespace ThesisManager.Core.Domain.Core {
    
    /// <summary>
    /// Basisklasse f�r alle Domainobjekte die eine ID als Eigenschaft haben.
    /// </summary>
    public abstract class DomainEntityWithId : DomainEntity {
        private readonly int _id;

        /// <summary>
        /// Erstellt eine neue Instanz eines DomainEntityWithId.
        /// </summary>
        protected DomainEntityWithId() {
        }

        /// <summary>
        /// Erstellt eine neue Instanz eines DomainEntityWithId mit einer definierten ID.
        /// Dieser Konstruktor ist eher f�r Testf�lle gedacht.
        /// </summary>
        /// <param name="id"></param>
        protected DomainEntityWithId(int id) {
            _id = id;
        }

        /// <summary>
        /// Ruft die Id des Objektes ab.
        /// </summary>
        public virtual int Id {
            get { return _id; }
        }
    }
}