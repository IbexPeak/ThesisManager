namespace ThesisManager.Core.Exceptions {
    using System;

    /// <summary>
    /// Exception die geworfen wird, wenn ein Wert doppelt vergeben ist und dies nicht erlaubt ist.
    /// </summary>
    public class DuplicateValueException : UserOperationException {
        private readonly string _propertyName;

        /// <summary>
        /// Erstellt eine neue Instanz der DuplicateValueException.
        /// </summary>
        /// <param name="propertyName">Der Name des Property der mit seinem Wert zu dieser Ausnahme geführt hat.</param>
        public DuplicateValueException(string propertyName) {
            _propertyName = propertyName;
        }

        /// <summary>
        /// Erzeugt eine neue Instanz der <see cref="DuplicateValueException"/>.
        /// </summary>
        /// <param name="propertyName">Der Name des Property der mit seinem Wert zu dieser Ausnahme geführt hat.</param>
        /// <param name="message">Die Nachricht, die diese Ausnahme beschreibt.</param>
        /// <param name="innerException">Die Exception die der Grund für diese Exception ist.</param>
        public DuplicateValueException(string propertyName, string message, Exception innerException)
                : base(message, innerException) {
            _propertyName = propertyName;
        }

        /// <summary>
        /// Liefert den Propertynamen und die Fehlermeldung, oder nur die Fehlermeldung, wenn kein Propertyname gesetzt ist.
        /// </summary>
        public override string Message {
            get {
                if (String.IsNullOrWhiteSpace(Property)) {
                    return base.Message;
                } else {
                    return string.Format("Der Wert für das Property {0} ist bereits vorhanden.{1}{2}",
                            Property,
                            Environment.NewLine,
                            base.Message);
                }
            }
        }

        /// <summary>
        /// Ruft den Namen des Properties ab, der doppelt vergeben werden soll.
        /// </summary>
        public string Property {
            get { return _propertyName; }
        }
    }
}
