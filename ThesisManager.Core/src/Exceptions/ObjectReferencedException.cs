namespace ThesisManager.Core.Exceptions {

    /// <summary>
    /// Exception die geworfen wird, wenn ein Objekt nicht gelöscht werden kann, weil es referenziert ist.
    /// </summary>
    public class ObjectReferencedException : UserOperationException {

        private readonly string _objectName;

        /// <summary>
        /// Erzeugt eine neue Instanz der ObjectReferencedException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="objectName"></param>
        public ObjectReferencedException(string message, string objectName)
            : base(message) {
            _objectName = objectName;
        }

        /// <summary>
        /// Ruft den Namen des Objekts ab, welches referenziert ist.
        /// </summary>
        public string ObjectName {
            get { return _objectName; }
        }


    }
}