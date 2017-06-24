namespace ThesisManager.Core.Persistence.Core.ActiveDirectory {
    /// <summary>
    ///   Schnittstelle f�r einen Dao, der Methoden zum Zugriff auf das Active Directory bietet.
    /// </summary>
    public interface IActiveDirectoryDao {

        /// <summary>
        ///   Ruft eine Eigenschaft eines ActiveDirectory-Nutzers ab. Existiert der Nutzer nicht oder ist kein Zugriff m�glich, wird eine leere Zeichenfolge zur�ckgegeben.
        /// </summary>
        /// <param name="username"> Der Nutzername f�r den die Eigenschaft abgerufen werden soll. </param>
        /// <param name="property"> Welche Eigenschaft des Nutzers soll abgerufen werden? </param>
        /// <returns> </returns>
        string GetProperty(string username, ActiveDirectoryUserProperty property);

        /// <summary>
        ///   Ruft ab, ob der angegebene Nutzername vorhanden ist oder nicht.
        /// </summary>
        /// <param name="username"> Der zu �berpr�fende Nutzername </param>
        /// <returns> true wenn der Name gefunden wurde, false wenn nicht </returns>
        bool IsExistingUsername(string username);

        /// <summary>
        ///   Ruft bestimmte Informationen �ber einen Nutzer ab.
        /// </summary>
        /// <param name="username"> Der Nutzer f�r den die Informationen abgerufen werden sollen. </param>
        /// <returns> K�nnen f�r den Nutzernamen keine Informationen abgerufen werden wird null, andernfalls ein Objekt mit Informationen geliefert. </returns>
        UserNtInformation FindUserInformation(string username);
    }
}