namespace ThesisManager.Core.Persistence.Core.ActiveDirectory {
    /// <summary>
    ///     Klasse f�r Erweiterungsmethoden.
    /// </summary>
    public static class ExtensionMethods {
        /// <summary>
        ///     Liefert eine Abk�rzung die f�r das Abfragen der Eigenschaft aus dem ActiveDirectory verwendet werden kann.
        ///     Die Abk�rzung wird �ber das <see cref="ActiveDirectoryUserPropertyAttribute" /> ermittelt/definiert.
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