﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.ModelTests;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.ModelTests
{

    [TestClass()]
    public class UserServiceTest
    {
        // Variables used in several tests are initialized here
        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";
        private const string country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;
        private static IKernel kernel;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;

        private TransactionScope transaction;

        private TestContext testContextInstance;

       

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the
        /// current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        /// A test for RegisterUser
        /// </summary>
        /// 

        [TestMethod()]
        public void RegisterUserTest()
        {
            // Register user and find profile
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);
                var userProfile = userProfileDao.Find(userId);

                // Check data
                Assert.AreEqual(userId, userProfile.usrId);
                Assert.AreEqual(loginName, userProfile.loginName);
                Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), userProfile.enPassword);
                Assert.AreEqual(firstName, userProfile.firstName);
                Assert.AreEqual(lastName, userProfile.lastName);
                Assert.AreEqual(email, userProfile.email);
                Assert.AreEqual(language, userProfile.language);
                Assert.AreEqual(country, userProfile.country);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for registering a user that already exists in the database
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedUserTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                // Register user
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // Register the same user
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [TestMethod]
        public void LoginClearPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                var expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), language, country);

                // Login with clear password
                var actual =
                    userService.Login(loginName,
                        clearPassword, false);

                // Check data
                Assert.AreEqual(expected, actual);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [TestMethod]
        public void LoginEncryptedPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                var expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), language, country);

                // Login with encrypted password
                var obtained =
                    userService.Login(loginName,
                        PasswordEncrypter.Crypt(clearPassword), true);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LoginIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // Login with incorrect (clear) password
                var actual =
                    userService.Login(loginName, clearPassword + "X", false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void LoginNonExistingUserTest()
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userService = kernel.Get<IUserService>();
            userProfileDao = kernel.Get<IUserProfileDao>();

            // Login for a user that has not been registered
            var actual =
                userService.Login(loginName, clearPassword, false);
        }

        /// <summary>
        /// A test for FindUserProfileDetails
        /// </summary>
        [TestMethod]
        public void FindUserProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                var expected =
                    new UserProfileDetails(firstName, lastName, email, language, country);

                var userId =
                    userService.RegisterUser(loginName, clearPassword, expected);

                var obtained =
                    userService.FindUserProfileDetails(userId);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for FindUserProfileDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindUserProfileDetailsForNonExistingUserTest()
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userService = kernel.Get<IUserService>();
            userProfileDao = kernel.Get<IUserProfileDao>();

            userService.FindUserProfileDetails(NON_EXISTENT_USER_ID);
        }

        /// <summary>
        /// A test for UpdateUserProfileDetails
        /// </summary>
        [TestMethod]
        public void UpdateUserProfileDetailsTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                // Register user and update profile details
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                var expected =
                    new UserProfileDetails(firstName + "X", lastName + "X",
                        email + "X", "XX", "XX");

                userService.UpdateUserProfileDetails(userId, expected);

                var obtained =
                    userService.FindUserProfileDetails(userId);

                // Check changes
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for UpdateUserProfileDetails when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateUserProfileDetailsForNonExistingUserTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                userService.UpdateUserProfileDetails(NON_EXISTENT_USER_ID,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for ChangePassword
        /// </summary>
        [TestMethod]
        public void ChangePasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // Change password
                var newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword, newClearPassword);

                // Try to login with the new password. If the login is correct, then the password
                // was successfully changed.
                userService.Login(loginName, newClearPassword, false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for ChangePassword entering a wrong old password
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void ChangePasswordWithIncorrectPasswordTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                // Register user
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // Change password
                var newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword + "Y", newClearPassword);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test for ChangePassword when the user does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ChangePasswordForNonExistingUserTest()
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userService = kernel.Get<IUserService>();
            userProfileDao = kernel.Get<IUserProfileDao>();

            userService.ChangePassword(NON_EXISTENT_USER_ID,
                clearPassword, clearPassword + "X");
        }

        /// <summary>
        /// A test to check if a valid user loginName is found
        /// </summary>
        [TestMethod]
        public void UserExistsForValidUser()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                // Register user
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                bool userExists = userService.UserExists(loginName);

                Assert.IsTrue(userExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test to check if a not valid user loginame is found
        /// </summary>
        [TestMethod]
        public void UserExistsForNotValidUser()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                String invalidLoginName = loginName + "_someFakeUserSuffix";

                bool userExists = userService.UserExists(invalidLoginName);

                Assert.IsFalse(userExists);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        /// A test to check if follow method works correctly
        /// </summary>
        [TestMethod]
        public void FollowTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                // Register user
                var userId1 = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // Register user
                var userId2 = userService.RegisterUser("user2", "1234",
                    new UserProfileDetails(firstName, lastName, email, language, country));

                userService.follow(loginName,"user2");
                UserProfile user1 = userProfileDao.FindByLoginName(loginName);
                UserProfile user2 = userProfileDao.FindByLoginName("user2");

                Assert.IsTrue(user1.UserProfile2.Contains(user2));
                Assert.IsTrue(user2.UserProfile1.Contains(user1));


                // transaction.Complete() is not called, so Rollback is executed.
            }
        }


        /// <summary>
        /// A test to check if follow method runs the exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void InvalidUserFollowTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                userProfileDao = kernel.Get<IUserProfileDao>();

                // Register user
                var userId2 = userService.RegisterUser("user2", "1234",
                    new UserProfileDetails(firstName, lastName, email, language, country));

                userService.follow("pepe", "user2");


                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

    }
}