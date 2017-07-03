using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThesisManager.Core.Persistence;
using ThesisManager.Core.Domain;
using System.Collections.Generic;
using System.Collections;

namespace ThesisManager.Core.Tests.src.Persistence
{
    [TestClass]
    public class UserDbDaoTests
    {
        private readonly IUserDbDao _userDbDao = new UserDbDao();


        [TestMethod]
        public void TestCreate()
        {
            // GIVEN: Ausgangsdaten

            string login = "s74022";
            UserType userType = UserType.Student;

            User user = new User(login, userType);

            // WHEN: Wir das OBjekt über den Dao erstellen und danach aus der DB holen

            _userDbDao.Save(user);
            _userDbDao.FlushAndClear();

            IList<User> users = _userDbDao.GetAll();

            // THEN: Sollte das mit den Ausgangsdaten da sein

            CollectionAssert.Contains((ICollection)users, user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreate_Fail()
        {
            // GIVEN: Ausgangsdaten mit null als Login

            string login = null;
            UserType userType = UserType.Student;

            User user = new User(login, userType);

            // WHEN: Wir das OBjekt über den Dao erstellen und danach aus der DB holen

            _userDbDao.Save(user);
            _userDbDao.FlushAndClear();

            IList<User> users = _userDbDao.GetAll();

            // THEN:Sollte eine ArgumentNullException zurückkommen
        }
    }
}
