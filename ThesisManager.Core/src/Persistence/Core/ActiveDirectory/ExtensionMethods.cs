namespace ThesisManager.Core.Persistence.Core.ActiveDirectory {
    /// <summary>
    ///     Klasse für Erweiterungsmethoden.
    /// </summary>
    public static class ExtensionMethods {
        /// <summary>
        ///     Liefert eine Abkürzung die für das Abfragen der Eigenschaft aus dem ActiveDirectory verwendet werden kann.
        ///     Die Abkürzung wird über das <see cref="ActiveDirectoryUserPropertyAttribute" /> ermittelt/definiert.
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetShortcut(this ActiveDirectoryUserProperty en) {
            ActiveDirectoryUserPropertyAttribute[] attributes =
                    (ActiveDirectoryUserPropertyAttribute[])en
                            .GetType().GetField(en.ToString()).GetCustomAttributes(typeof(ActiveDirectoryUserPropertyAttribute), false);
            if (attributes.Length > 0) {
                return attributes[0].Shortcut;
            }

            return "";
        }
    }
}