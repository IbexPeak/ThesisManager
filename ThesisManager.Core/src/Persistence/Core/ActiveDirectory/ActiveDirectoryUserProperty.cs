namespace ThesisManager.Core.Persistence.Core.ActiveDirectory {
    using System.ComponentModel;

    /// <summary>
    ///     Enumeration der Eigenschaften die aus dem ActiveDirectory f�r einen Nutzer ausgelesen werden k�nnen.
    ///     Sollten weitere Attribute hinzugef�gt werden m�ssen, k�nnen diese hier nachgelesen werden:
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms675090%28v=vs.85%29.aspx
    /// </summary>
    public enum ActiveDirectoryUserProperty {
        /// <summary>
        ///     Abfrage des Vollst�ndigen Namens eines Benutzers.
        /// </summary>
        [Description("Voller Name des Benutzers")]
        [ActiveDirectoryUserProperty("cn")]
        LoginName,

        /// <summary>
        ///     Abfrage des Vornamens eines Benutzers.
        /// </summary>
        [Description("Vorname des Benutzers")]
        [ActiveDirectoryUserProperty("givenName")]
        FirstName,

        /// <summary>
        ///     Abfrage des Nachnamens eines Benutzers.
        /// </summary>
        [Description("Nachname des Benutzers")]
        [ActiveDirectoryUserProperty("sn")]
        LastName,

        /// <summary>
        ///     Abfrage der Mail-Adresse eines AD-Benutzers.
        /// </summary>
        [Description("Mail-Adresse des Benutzers")]
        [ActiveDirectoryUserProperty("mail")]
        Mail,

        /// <summary>
        ///     Abfrage des Firmennamens eines AD-Benutzers.
        /// </summary>
        [Description("Unternehmen des Benutzers")]
        [ActiveDirectoryUserProperty("company")]
        Company,

        /// <summary>
        ///     Abfrage des Namens der Abteilung eines AD-Benutzers.
        /// </summary>
        [Description("Abteilung des Benutzers")]
        [ActiveDirectoryUserProperty("department")]
        Department,

        /// <summary>
        ///     Abfrage der Telefonnummer eines AD-Benutzers.
        /// </summary>
        [Description("Telefonnummer")]
        [ActiveDirectoryUserProperty("telephoneNumber")]
        Phone
    }
}