namespace ThesisManager.Core.Exceptions {
    using System;

    /// <summary>
    /// Basisklasse für alle Exceptions die durch fehlerhafte Nutzereingaben hervorgerufen werden.
    /// </summary>
    public abstract class UserOperationException : Exception {
        
        /// <summary>
        /// Erstellt eine neue UserOperationException
        /// </summary>
        public UserOperationException() : base() {
            
        }

        /// <summary>
        /// Erstellt eine neue UserOperationException mit Fehlertext
        /// </summary>
        /// <param name="message">Die Fehlernachricht.</param>
        public UserOperationException(string message) : base(message) {
            
        }

        /// <summary>
        /// Erstellt eine neue UserOperationException mit Fehlertext und InnerException.
        /// </summary>
        /// <param name="message">Der Fehlertext.</param>
        /// <param name="innerException">Die InnerException mit weiteren Details zur aufgetretenen Ausnahme.</param>
        public UserOperationException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}