namespace ThesisManager.Web.Models.User {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using ThesisManager.Core.Domain;

    public class UserViewModel {
        private readonly DateTime? _birthday;
        private readonly string _city;
        private readonly string _email;
        private readonly string _firstname;
        private readonly string _housenumber;
        private readonly string _lastname;
        private readonly string _login;
        private readonly string _phone;
        private readonly string _street;
        private readonly string _zipcode;
        
        private UserType _userType;

        /// <summary>
        ///     Konstruktur zum Erstellen oder Aktualisieren eines Nutzers.
        /// </summary>
        /// <param name="user">Der Nutzer</param>
        public UserViewModel(User user) {
            _login = user.Login;

            SelectableUserTypes = Enum.GetValues(typeof(UserType)).Cast<UserType>().ToList();

            _birthday = user.Birthday;
            _city = user.City;
            _email = user.Email;
            _firstname = user.Firstname;
            _housenumber = user.Housenumber;
            _lastname = user.Lastname;
            _phone = user.Phone;
            _street = user.Street;
            _zipcode = user.Zipcode;

            _userType = user.UserType;
        }

        public IList<UserType> SelectableUserTypes { get; private set; }

        [Display(Name = "Geburtsdatum")]
        public DateTime? Birthday => _birthday;

        [Display(Name = "Stadt")]
        public string City => _city;

        [Display(Name = "Email")]
        public string Email => _email;

        [Display(Name = "Vorname")]
        public string Firstname => _firstname;

        [Display(Name = "Hausnummer")]
        public string Housenumber => _housenumber;

        [Display(Name = "Nachname")]
        public string Lastname => _lastname;

        [Display(Name = "Login")]
        public string Login => _login;

        [Display(Name = "Telefon")]
        public string Phone => _phone;

        [Display(Name = "Straße")]
        public string Street => _street;

        [Display(Name = "Nutzertyp")]
        public UserType UserType {
            get { return _userType; }
            set { _userType = value; }
        }

        [Display(Name = "PLZ")]
        public string Zipcode => _zipcode;
    }
}