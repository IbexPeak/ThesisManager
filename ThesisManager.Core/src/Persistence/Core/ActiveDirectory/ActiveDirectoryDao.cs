namespace ThesisManager.Core.Persistence.Core.ActiveDirectory {
    using System;
    using System.DirectoryServices;

    /// <summary>
    /// Klasse die den Zugriff auf das Active Directory erlaubt.
    /// </summary>
    public class ActiveDirectoryDao : IActiveDirectoryDao {
        
        /// <summary>
        /// Ruft eine Eigenschaft eines ActiveDirectory-Nutzers ab.
        /// Existiert der Nutzer nicht oder ist kein Zugriff möglich, wird eine leere Zeichenfolge zurückgegeben.
        /// </summary>
        /// <param name="username">Der Nutzername für den die Eigenschaft abgerufen werden soll.</param>
        /// <param name="property">Welche Eigenschaft des Nutzers soll abgerufen werden?</param>
        /// <returns></returns>
        public string GetProperty(string username, ActiveDirectoryUserProperty property) {

            return GetProperty(username, property.GetShortcut());
        }

        /// <summary>
        /// Ruft eine Eigenschaft eines ActiveDirectory-Nutzers ab.
        /// Existiert der Nutzer nicht oder ist kein Zugriff möglich, wird eine leere Zeichenfolge zurückgegeben.
        /// </summary>
        /// <param name="username">Der Nutzername für den die Eigenschaft abgerufen werden soll.</param>
        /// <param name="property">Welche Eigenschaft des Nutzers soll abgerufen werden?</param>
        /// <returns></returns>
        public string GetProperty(string username, string property) {

            string result = "";

            try {
                DirectorySearcher directorySearcher = new DirectorySearcher();
                directorySearcher.Filter = String.Format("(SAMAccountName={0})", username);
                directorySearcher.PropertiesToLoad.Add(property);

                SearchResult searchResult = directorySearcher.FindOne();

                int propertyEntryCount = searchResult.Properties[property].Count;
                if (propertyEntryCount>0) {
                    int lastPropertyEntryIndex = propertyEntryCount - 1;
                    result = searchResult.Properties[property][lastPropertyEntryIndex].ToString();
                }
            } catch (Exception) {
                result = "";
            }

            return result;
        }

        /// <summary>
        /// Ruft bestimmte Informationen über einen Nutzer ab.
        /// </summary>
        /// <param name="username">Der Nutzer für den die Informationen abgerufen werden sollen.</param>
        /// <returns></returns>
        public UserNtInformation FindUserInformation(string username) {

            try {
                string firstName = GetProperty(username, ActiveDirectoryUserProperty.FirstName);
                string lastName = GetProperty(username, ActiveDirectoryUserProperty.LastName);
                string company = GetProperty(username, ActiveDirectoryUserProperty.Company);
                string department = GetProperty(username, ActiveDirectoryUserProperty.Department);
                string email = GetProperty(username, ActiveDirectoryUserProperty.Mail);
                string phone = GetProperty(username, ActiveDirectoryUserProperty.Phone);


                return new UserNtInformation(firstName, lastName, company, department, email, phone);
            } catch (Exception) {
                return null;
            }
        }

        /// <summary>
        /// Ruft ab, ob der angegebene Nutzername vorhanden ist oder nicht.
        /// </summary>
        /// <param name="username">Der zu überprüfende Nutzername</param>
        /// <returns>true wenn der Name gefunden wurde, false wenn nicht</returns>
        public bool IsExistingUsername(string username) {
            DirectorySearcher directorySearcher = new DirectorySearcher();
            directorySearcher.Filter = String.Format("(SAMAccountName={0})", username);
            return directorySearcher.FindOne() != null;
        }
    }
}

