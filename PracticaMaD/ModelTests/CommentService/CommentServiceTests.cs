using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.PublicationService;
using Es.Udc.DotNet.PracticaMaD.Model.PublicationDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.ModelTests;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService.Tests
{
    [TestClass()]
    public class CommentServiceTests
    {
        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";
        private const string country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;
        private const int startindex = 0;
        private const int count = 10;
        private static IKernel kernel;
        private static IImageUploadService imageUploadService;
        private static IImageUploadDao imageUploadDao;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static IPublicationService publicationService;
        private static IPublicationDao publicationDao;
        private static ICommentService commentService;
        private static ICommentDao commentDao;


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
        public void AddCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationService = kernel.Get<IPublicationService>();
                commentService = kernel.Get<ICommentService>();
                commentDao = kernel.Get<ICommentDao>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", "Description",
                    DateTime.Now, 1, 1, "ISO", "wb", "category");

                long imgId = imageUploadService.UploadImage(img);
                long pubId = publicationService.UploadPublication(userId, imgId);

                long commentId1 = commentService.AddComment(pubId, "Commentary1", userId);
                long commentId2 = commentService.AddComment(pubId, "Commentary2", userId);

                Comment comment1 = commentDao.Find(commentId1);
                Comment comment2 = commentDao.Find(commentId2);

                Assert.IsTrue(comment1.content.Equals("Commentary1"));
                Assert.IsTrue(comment2.content.Equals("Commentary2"));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        public void ShowCommentsTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationService = kernel.Get<IPublicationService>();
                commentService = kernel.Get<ICommentService>();
                commentDao = kernel.Get<ICommentDao>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", "Description",
                    DateTime.Now, 1, 1, "ISO", "wb", "category");

                long imgId = imageUploadService.UploadImage(img);
                long pubId = publicationService.UploadPublication(userId, imgId);

                long commentId1 = commentService.AddComment(pubId, "Commentary1", userId);
                long commentId2 = commentService.AddComment(pubId, "Commentary2", userId);

                Comment comment1 = commentDao.Find(commentId1);
                Comment comment2 = commentDao.Find(commentId2);

                Assert.IsTrue(comment1.content.Equals("Commentary1"));
                Assert.IsTrue(comment2.content.Equals("Commentary2"));

                List<Comment> result = commentService.ShowComments(pubId, startindex, count);

                Assert.IsTrue(result.Contains(comment1));
                Assert.IsTrue(result.Contains(comment2));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        public void UpdateCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationService = kernel.Get<IPublicationService>();
                commentService = kernel.Get<ICommentService>();
                commentDao = kernel.Get<ICommentDao>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", "Description",
                    DateTime.Now, 1, 1, "ISO", "wb", "category");

                long imgId = imageUploadService.UploadImage(img);
                long pubId = publicationService.UploadPublication(userId, imgId);

                long commentId1 = commentService.AddComment(pubId, "Commentary1", userId);
                long commentId2 = commentService.AddComment(pubId, "Commentary2", userId);

                Comment comment1 = commentDao.Find(commentId1);
                Comment comment2 = commentDao.Find(commentId2);

                Assert.IsTrue(comment1.content.Equals("Commentary1"));
                Assert.IsTrue(comment2.content.Equals("Commentary2"));

                commentService.UpdateComment(commentId1, "ModifiedComentary1");
                comment1 = commentDao.Find(commentId1);
                Assert.IsTrue(comment1.content.Equals("ModifiedComentary1"));


                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateInvalidCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationService = kernel.Get<IPublicationService>();
                commentService = kernel.Get<ICommentService>();
                commentDao = kernel.Get<ICommentDao>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", "Description",
                    DateTime.Now, 1, 1, "ISO", "wb", "category");

                long imgId = imageUploadService.UploadImage(img);
                long pubId = publicationService.UploadPublication(userId, imgId);

                long commentId1 = commentService.AddComment(pubId, "Commentary1", userId);
                long commentId2 = commentService.AddComment(pubId, "Commentary2", userId);

                Comment comment1 = commentDao.Find(commentId1);
                Comment comment2 = commentDao.Find(commentId2);

                Assert.IsTrue(comment1.content.Equals("Commentary1"));
                Assert.IsTrue(comment2.content.Equals("Commentary2"));

                commentService.RemoveComment(commentId1);
                commentService.UpdateComment(commentId1, "ModifiedComentary1");
                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ShowCommentsInvalidPublicationTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationService = kernel.Get<IPublicationService>();
                commentService = kernel.Get<ICommentService>();
                commentDao = kernel.Get<ICommentDao>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", "Description",
                    DateTime.Now, 1, 1, "ISO", "wb", "category");

                long imgId = imageUploadService.UploadImage(img);
                long pubId = publicationService.UploadPublication(userId, imgId);

                commentService.ShowComments(-1,startindex,count);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        public void RemoveCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationService = kernel.Get<IPublicationService>();
                commentService = kernel.Get<ICommentService>();
                commentDao = kernel.Get<ICommentDao>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", "Description",
                    DateTime.Now, 1, 1, "ISO", "wb", "category");

                long imgId = imageUploadService.UploadImage(img);
                long pubId = publicationService.UploadPublication(userId, imgId);

                long commentId1 = commentService.AddComment(pubId, "Commentary1", userId);
                long commentId2 = commentService.AddComment(pubId, "Commentary2", userId);

                Comment comment1 = commentDao.Find(commentId1);
                Comment comment2 = commentDao.Find(commentId2);

                Assert.IsTrue(comment1.content.Equals("Commentary1"));
                Assert.IsTrue(comment2.content.Equals("Commentary2"));

                List<Comment> result = commentService.ShowComments(pubId, startindex, count);

                Assert.IsTrue(result.Contains(comment1));
                Assert.IsTrue(result.Contains(comment2));

                commentService.RemoveComment(commentId1);
                result = commentService.ShowComments(pubId, startindex, count);
                Assert.IsTrue(result.Contains(comment2));
                Assert.IsFalse(result.Contains(comment1));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void RemoveInvalidCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                userService = kernel.Get<IUserService>();
                publicationService = kernel.Get<IPublicationService>();
                commentService = kernel.Get<ICommentService>();
                commentDao = kernel.Get<ICommentDao>();
                imageUploadService = kernel.Get<IImageUploadService>();

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", "Description",
                    DateTime.Now, 1, 1, "ISO", "wb", "category");

                long imgId = imageUploadService.UploadImage(img);
                long pubId = publicationService.UploadPublication(userId, imgId);

                long commentId1 = commentService.AddComment(pubId, "Commentary1", userId);
                long commentId2 = commentService.AddComment(pubId, "Commentary2", userId);

                Comment comment1 = commentDao.Find(commentId1);
                Comment comment2 = commentDao.Find(commentId2);

                Assert.IsTrue(comment1.content.Equals("Commentary1"));
                Assert.IsTrue(comment2.content.Equals("Commentary2"));

                List<Comment> result = commentService.ShowComments(pubId, startindex, count);

                Assert.IsTrue(result.Contains(comment1));
                Assert.IsTrue(result.Contains(comment2));

                commentService.RemoveComment(commentId1);
                result = commentService.ShowComments(pubId, startindex, count);
                Assert.IsTrue(result.Contains(comment2));
                Assert.IsFalse(result.Contains(comment1));
                commentService.RemoveComment(commentId1);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }
    }
}