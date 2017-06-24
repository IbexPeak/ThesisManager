namespace ThesisManager.Core.Domain {
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    ///     Bildet einen Benutzer ab. Manche Properties kommen aus der DB, andere aus dem LDAP.
    /// </summary>
    public class User : DomainEntityWithId, IPrincipal, IIdentity {
        private readonly bool _isAuthenticated;
        private readonly string _password;
        private DateTime? _birthday;
        private string _city;
        private string _email;
        private string _firstname;
        private string _housenumber;
        private string _lastname;
        private string _phone;
        private string _street;

        private IList<UserPermission> _userPermissions = new List<UserPermission>();
        private UserType _userType;
        private string _zipcode;

        /// <summary>
        ///     Konstruktur zum Erstellen eines Nutzers für die DB.
        /// </summary>
        /// <param name="login">Der Login des Nutzers</param>
        /// <param name="userType">Der Typ des Nutzers</param>
        /// <param name="userPermissions">Die Liste der Berechtigungen des Nutzers</param>
        public User(string login, UserType userType, IList<UserPermission> userPermissions) {
            if (login == null) {
                throw new ArgumentNullException(nameof(login));
            }
            if (userPermissions == null) {
                throw new ArgumentNullException(nameof(userPermissions));
            }
            Login = login;
            UpdateUser(userType, userPermissions);
        }

        /// <summary>
        ///     Konstruktur zum Erstellen eines Nutzers für das LDAP.
        /// </summary>
        public User(string login, string firstname, string lastname, string street, string housenumber, string zipcode, string city,
                    string email, string phone, DateTime birthday, string password) {
            Login = login;

            _password = password;
            SetupUser(firstname, lastname, street, housenumber, zipcode, city, email, phone, birthday);
        }

        /// <summary>
        ///     Hibernate-Konstruktor.
        /// </summary>
        protected User() {
        }

        public string AuthenticationType {
            get { return null; }
        }

        /// <summary>
        ///     Liefert das Geburtsdatum. Kommt aus dem LDAP.
        /// </summary>
        public DateTime? Birthday => _birthday;

        /// <summary>
        ///     Liefert die Stadt. Kommt aus dem LDAP.
        /// </summary>
        public string City => _city;

        /// <summary>
        ///     Liefert die Email. Kommt aus dem LDAP.
        /// </summary>
        public string Email => _email;

        /// <summary>
        ///     Liefert den Vornamen. Kommt aus dem LDAP.
        /// </summary>
        public string Firstname => _firstname;

        /// <summary>
        ///     Liefert die Hausnummer. Kommt aus dem LDAP.
        /// </summary>
        public string Housenumber => _housenumber;

        public IIdentity Identity {
            get { return this; }
        }

        public bool IsAuthenticated {
            get { return _isAuthenticated; }
        }

        /// <summary>
        ///     Liefert den Nachnamen. Kommt aus dem LDAP.
        /// </summary>
        public string Lastname => _lastname;

        /// <summary>
        ///     Liefert den Login. Kommt aus der DB und dem LDAP.
        /// </summary>
        public string Login { get; }

        public string Name {
            get { return Login; }
        }

        /// <summary>
        ///     Liefert das Passwort des Nutzers. Kommt aus dem Ldap.
        /// </summary>
        public string Password => _password;

        /// <summary>
        ///     Liefert die Telefonnnummer. Kommt aus dem LDAP.
        /// </summary>
        public string Phone => _phone;

        /// <summary>
        ///     Liefert die Straße. Kommt aus dem LDAP.
        /// </summary>
        public string Street => _street;

        /// <summary>
        ///     Liefert die Rechte des Nutzers. Kommt aus der Datenbank.
        /// </summary>
        public IList<UserPermission> UserPermissions => _userPermissions;

        /// <summary>
        ///     Liefert den Nutzertyp. Kommt aus der DB. Wird vom Administrator gepflegt.
        /// </summary>
        public UserType UserType => _userType;

        /// <summary>
        ///     Liefert die PLZ. Kommt aus dem LDAP.
        /// </summary>
        public string Zipcode => _zipcode;

        public bool IsInRole(string role) {
            return _userType.ToString() == role;
        }

        /// <summary>
        ///     Trägt die LDAP-Daten eines Nutzers ein.
        /// </summary>
        /// <param name="firstname">Vorname</param>
        /// <param name="lastname">Nachname</param>
        /// <param name="street">Straße</param>
        /// <param name="housenumber">Hausnummer</param>
        /// <param name="zipcode">PLZ</param>
        /// <param name="city">Stadt</param>
        /// <param name="email">Mail</param>
        /// <param name="phone">Telefon</param>
        /// <param name="birthday">Geb.-Datum</param>
        /// <param name="password">LDAP-Passwort</param>
        public void SetupUser(string firstname, string lastname, string street, string housenumber, string zipcode, string city,
                              string email, string phone, DateTime? birthday) {
            _birthday = birthday;
            _city = city;
            _email = email;
            _firstname = firstname;
            _housenumber = housenumber;
            _lastname = lastname;
            _phone = phone;
            _street = street;
            _zipcode = zipcode;
        }

        /// <summary>
        ///     Aktualisiert einen Nutzer in der DB.
        /// </summary>
        /// <param name="userType">Der Typ des Nutzers</param>
        /// <param name="userPermissions">Die Liste der Berechtigungen des Nutzers</param>
        public void UpdateUser(UserType userType, IList<UserPermission> userPermissions) {
            if (userPermissions == null) {
                throw new ArgumentNullException(nameof(userPermissions));
            }
            _userType = userType;
            _userPermissions = userPermissions;
        }
    }
}