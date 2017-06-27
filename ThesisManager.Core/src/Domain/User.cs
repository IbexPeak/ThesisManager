namespace ThesisManager.Core.Domain {
    using System;
    using System.Security.Principal;

    using ThesisManager.Core.Domain.Core;

    /// <summary>
    ///     Bildet einen Benutzer ab. Manche Properties kommen aus der DB, andere aus dem LDAP.
    /// </summary>
    public class User : DomainEntityWithId, IPrincipal, IIdentity {
        private readonly DateTime? _birthday;
        private readonly string _city;
        private readonly string _email;
        private readonly string _firstname;
        private readonly string _housenumber;
        private readonly bool _isAuthenticated;
        private readonly string _lastname;
        private readonly string _login;
        private readonly string _password;
        private readonly string _phone;
        private readonly string _street;
        private readonly string _zipcode;

        private UserType _userType;

        /// <summary>
        ///     Konstruktur zum Erstellen eines Nutzers für die DB.
        /// </summary>
        /// <param name="login">Der Login des Nutzers</param>
        /// <param name="userType">Der Typ des Nutzers</param>
        public User(string login, UserType userType) {
            if (login == null) {
                throw new ArgumentNullException(nameof(login));
            }
            _login = login;
            UpdateUser(userType);
        }

        /// <summary>
        ///     Konstruktur zum Erstellen eines Nutzers für das LDAP.
        /// </summary>
        public User(string login, string firstname, string lastname, string street, string housenumber, string zipcode, string city,
                    string email, string phone, DateTime birthday, string password) {
            _login = login;

            _password = password;

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
        public string Login {
            get { return _login; }
        }

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
        ///     Aktualisiert einen Nutzer in der DB.
        /// </summary>
        /// <param name="userType">Der Typ des Nutzers</param>
        public void UpdateUser(UserType userType) {
            _userType = userType;
        }
    }
}