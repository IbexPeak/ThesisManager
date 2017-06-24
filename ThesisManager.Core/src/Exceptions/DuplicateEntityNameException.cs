namespace ThesisManager.Core.Exceptions {
    
    /// <summary>
    /// Exception die geworfen wird, wenn ein Objekt mit demselben Namen existiert, der Name aber eindeutig sein muss.
    /// </summary>
    public class DuplicateEntityNameException : DuplicateValueException {

        private readonly string _nameThatsDuplicate;

        /// <summary>
        /// Erzeugt eine neue Instanz der DuplicateEntityNameException-Klasse.
        /// </summary>
        /// <param name="propertyName">Der Name des Parameters</param>
        /// <param name="nameThatsDuplicate">Der Name der doppelt ist.</param>
        public DuplicateEntityNameException(string propertyName, string nameThatsDuplicate) : base(propertyName) {
            _nameThatsDuplicate = nameThatsDuplicate;
        }

        /// <summary>
        /// Ruft den doppelten Namen ab.
        /// </summary>
        public string DuplicateName {
            get { return _nameThatsDuplicate; }
        }
    }
}