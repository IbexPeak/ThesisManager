namespace ThesisManager.Core.Persistence {
    using System;
    using System.Collections.Generic;

    using ThesisManager.Core.Domain;

    public class TestData {

        private bool _isTestDataGenerated;

        public IUserDbDao UserDbDao { private get; set; }

        public IUserLdapDao UserLdapDao { private get; set; }

        public void GenerateTestData() {
            if (_isTestDataGenerated) {
                return;
            }
            ClearTestData();

            string login1 = "s74022";
            string login2 = "munkelt@htw-dresden.de";
            string login3 = "s74000";
            string login4 = "s74001";

            User ldapUser1 = new User(login1,
                                      "Felix",
                                      "Löschau",
                                      "Straße1",
                                      "1",
                                      "0815",
                                      "Stadt1",
                                      "s74022@htw-dresden.de",
                                      "0192401284902",
                                      new DateTime(1991, 1, 1),
                                      "passwort1");
            User ldapUser2 = new User(login2,
                                      "Thorsten",
                                      "Munkelt",
                                      "Straße2",
                                      "2",
                                      "0816",
                                      "Stadt2",
                                      login2,
                                      "0234908234823",
                                      new DateTime(1971, 1, 1),
                                      "passwort2");
            User ldapUser3 = new User(login3,
                                      "Philipp",
                                      "Nowak",
                                      "Straße3",
                                      "3",
                                      "0817",
                                      "Stadt3",
                                      "s74000@htw-dresden.de",
                                      "019240123524902",
                                      new DateTime(1990, 1, 1),
                                      "passwort3");
            User ldapUser4 = new User(login4,
                                      "Pitti",
                                      "Platsch",
                                      "Straße4",
                                      "4",
                                      "0818",
                                      "Stadt4",
                                      "s74001@htw-dresden.de",
                                      "0192401235235235",
                                      new DateTime(1989, 1, 1),
                                      "passwort4");

            UserLdapDao.Save(ldapUser1);
            UserLdapDao.Save(ldapUser2);
            UserLdapDao.Save(ldapUser3);
            UserLdapDao.Save(ldapUser4);

            User dbUser1 = new User(login1, UserType.Student, new List<UserPermission>() { UserPermission.CanViewUsers });
            User dbUser2 = new User(login2,
                                    UserType.Professor,
                                    new List<UserPermission>() { UserPermission.CanViewUsers, UserPermission.CanManageUsers });

            UserDbDao.Save(dbUser1);
            UserDbDao.Save(dbUser2);

            _isTestDataGenerated = true;
        }

        private void ClearTestData() {
            foreach (User user in UserLdapDao.GetAll()) {
                UserLdapDao.Delete(user);
            }
            foreach (User user in UserDbDao.GetAll()) {
                UserDbDao.Delete(user);
            }
        }
    }
}