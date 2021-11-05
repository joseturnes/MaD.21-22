using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.PublicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.PublicationDao;
using Es.Udc.DotNet.PracticaMaD.ModelTests;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.PublicationService.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.PublicationService.Test
{
    [TestClass()]
    public class PublicationServiceTest
    {
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
        private static IImageUploadService imageUploadService;
        private static IPublicationService publicationService;
        private static IPublicationDao publicationDao;

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

        [TestMethod()]
        public void FindPublicationTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationDao = kernel.Get<IPublicationDao>();
                publicationService = kernel.Get<IPublicationService>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user1 = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId1 = userService.RegisterUser(loginName, clearPassword, user1);

                ImageUploadDetails img = new ImageUploadDetails("arboles", "Arboles otoñales", DateTime.Now, 1, 1, "wb", "category");

                long imgId1 = imageUploadService.UploadImage(img);

                long pubId1 = publicationService.UploadPublication(userId1, imgId1);

                UserProfileDetails user2 = new UserProfileDetails("pepe", "perez", email, language, country);
                long userId2 = userService.RegisterUser("pepeperez", clearPassword, user2);

                ImageUploadDetails img2 = new ImageUploadDetails("cascadas", "Cascadas", DateTime.Now, 1, 1, "wb", "category");

                long imgId2 = imageUploadService.UploadImage(img);

                long pubId2 = publicationService.UploadPublication(userId2, imgId2);

                Assert.IsFalse(publicationDao.Find(pubId1).Equals(publicationDao.Find(pubId2)));



                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        public void LikedPublicationTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationDao = kernel.Get<IPublicationDao>();
                publicationService = kernel.Get<IPublicationService>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                UserProfileDetails user2 = new UserProfileDetails("pepe", "perez", email, language, country);
                long userId2 = userService.RegisterUser("pepeperez", clearPassword, user2);

                ImageUploadDetails img = new ImageUploadDetails("arboles", "Arboles otoñales", DateTime.Now, 1, 1, "wb", "category");

                long imgId = imageUploadService.UploadImage(img);

                long pubId = publicationService.UploadPublication(userId, imgId);

                publicationService.LikedPublication(pubId, userId);

                Assert.IsTrue(publicationDao.Find(pubId).likes == 1);

                publicationService.LikedPublication(pubId, userId2);

                Assert.IsTrue(publicationDao.Find(pubId).likes == 2);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(AlreadyLikedException))]
        public void AlreadyLikedPublicationTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationDao = kernel.Get<IPublicationDao>();
                publicationService = kernel.Get<IPublicationService>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                UserProfileDetails user2 = new UserProfileDetails("pepe", "perez", email, language, country);
                long userId2 = userService.RegisterUser("pepeperez", clearPassword, user2);

                ImageUploadDetails img = new ImageUploadDetails("arboles", "Arboles otoñales", DateTime.Now, 1, 1, "wb", "category");

                long imgId = imageUploadService.UploadImage(img);

                long pubId = publicationService.UploadPublication(userId, imgId);

                publicationService.LikedPublication(pubId, userId);

                publicationService.LikedPublication(pubId, userId2);

                Assert.IsTrue(publicationDao.Find(pubId).likes == 2);

                publicationService.LikedPublication(pubId, userId2);


                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        public void UpdatePublicationTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationDao = kernel.Get<IPublicationDao>();
                publicationService = kernel.Get<IPublicationService>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("arboles", "Arboles otoñales", DateTime.Now, 1, 1, "wb", "category");

                long imgId = imageUploadService.UploadImage(img);

                ImageUploadDetails img2 = new ImageUploadDetails("cascadas", "Cascadas", DateTime.Now, 1, 1, "wb", "category");

                long imgId2 = imageUploadService.UploadImage(img2);

                long pubId = publicationService.UploadPublication(userId, imgId);

                PublicationDetails pub = new PublicationDetails(imgId2,userId,0,DateTime.Now);

                Assert.IsTrue(publicationDao.Find(pubId).imgId == imgId);

                publicationService.UpdatePublication(pubId, pub);

                Assert.IsTrue(publicationDao.Find(pubId).imgId == imgId2);



                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        public void UploadPublicationTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationDao = kernel.Get<IPublicationDao>();
                publicationService = kernel.Get<IPublicationService>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);
                
                ImageUploadDetails img = new ImageUploadDetails("arboles", "Arboles otoñales", DateTime.Now,1,1,"wb","category");
                
                long imgId = imageUploadService.UploadImage(img);

                long pubId = publicationService.UploadPublication(userId,imgId);

                Assert.IsTrue(publicationDao.Find(pubId).pubId==pubId);



                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RemovePublicationTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationDao = kernel.Get<IPublicationDao>();
                publicationService = kernel.Get<IPublicationService>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("arboles", "Arboles otoñales", DateTime.Now, 1, 1, "wb", "category");

                long imgId = imageUploadService.UploadImage(img);

                long pubId = publicationService.UploadPublication(userId, imgId);

                publicationService.RemovePublication(pubId);

                Assert.IsFalse(publicationDao.Find(pubId).pubId == pubId);




                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RemoveInvalidPublicationTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationDao = kernel.Get<IPublicationDao>();
                publicationService = kernel.Get<IPublicationService>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("arboles", "Arboles otoñales", DateTime.Now, 1, 1, "wb", "category");

                long imgId = imageUploadService.UploadImage(img);

                long pubId = publicationService.UploadPublication(userId, imgId);

                publicationService.RemovePublication(pubId+1);

                Assert.IsFalse(publicationDao.Find(pubId).pubId == pubId);




                // transaction.Complete() is not called, so Rollback is executed.
            }
        }
    }
}