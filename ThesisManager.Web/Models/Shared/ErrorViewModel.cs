namespace ThesisManager.Web.Models.Shared {
    
    /// <summary>
    /// ViewModel für eine Fehleransicht, auf der eine Überschrift für einen Fehler und eine Beschreibung des Fehlers angezeigt werden.
    /// </summary>
    public class ErrorViewModel {

        public ErrorViewModel(string errorMessage) {
            ErrorTitle = "Fehler";
            ErrorMessage = errorMessage;
        }

        public ErrorViewModel(string errorTitle, string errorMessage) {
            ErrorTitle = errorTitle;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Ruft den Titel des Fehlers ab. 
        /// </summary>
        public string ErrorTitle { get; private set; }

        /// <summary>
        /// Ruft den anzuzeigenden Fehlertext ab.
        /// </summary>
        public string ErrorMessage { get; private set; }
    }
}