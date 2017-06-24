namespace ThesisManager.Core.Persistence {
    using System;
    using System.Collections.Generic;

    using ThesisManager.Core.Domain;

    public class TestData {
        public IUserDao UserDao { private get; set; }

        public ILdapDao UserLdapDao { private get; set; }

        public void GenerateTestData() {
            ClearTestData();

            string login1 = "s74022";
            string login2 = "munkelt@htw-dresden.de";

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

            UserLdapDao.Save(ldapUser1);
            UserLdapDao.Save(ldapUser2);

            User dbUser1 = new User(login1, UserType.Student, new List<UserPermission>() { UserPermission.CanViewUsers });
            User dbUser2 = new User(login2,
                                    UserType.Professor,
                                    new List<UserPermission>() { UserPermission.CanViewUsers, UserPermission.CanManageUsers });

            UserDao.Save(dbUser1);
            UserDao.Save(dbUser2);
        }

        private void ClearTestData() {
            foreach (User user in UserLdapDao.GetAll()) {
                UserLdapDao.Delete(user);
            }
            foreach (User user in UserDao.GetAll()) {
                UserDao.Delete(user);
            }
        }
    }
}