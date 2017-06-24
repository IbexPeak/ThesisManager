namespace ThesisManager.Web.Models.Shared {
    /// <summary>
    /// ViewModel für eine MessageView (egal ob Partial oder nicht), die einen Hinweis und einen Titel anzeigt.
    /// </summary>
    public class MessageViewModel {

        public MessageViewModel(string title, string message) {
            Title = title;
            Message = message;
        }

        /// <summary>
        /// Ruft den Titel der Hinweisseite ab.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Ruft den anzuzeigenden Hinweistext ab.
        /// </summary>
        public string Message { get; private set; }
    }
}