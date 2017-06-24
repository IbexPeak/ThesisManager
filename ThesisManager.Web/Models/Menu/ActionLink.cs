namespace ThesisManager.Web.Models.Menu {
    /// <summary>
    ///     Bildet einen Link ab,
    /// </summary>
    public class ActionLink : ActionUrl {
        public ActionLink(string text, string area, string controller, string action, bool isModal = false) : base(area, controller, action) {
            Text = text;
            IsModal = isModal;
        }

        /// <summary>
        /// Ruft den Text ab, der für den Link angezeigt wird.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Ruft ab , ob der Link modal geöffnet werden soll oder nicht.
        /// </summary>
        public bool IsModal { get; private set; }
        
    }
}