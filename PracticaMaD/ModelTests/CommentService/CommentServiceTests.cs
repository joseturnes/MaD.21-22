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
        private const float f1 = 1;
        private const float f2 = 1;
        private static IKernel kernel;
        private static IImageUploadService imageUploadService;
        private static IImageUploadDao imageUploadDao;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static ICommentService commentService;
        private static ICommentDao commentDao;


        private TransactionScope transaction;

        private TestContext testContextInstance;

        private void initializeKernel()
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userService = kernel.Get<IUserService>();
            commentService = kernel.Get<ICommentService>();
            commentDao = kernel.Get<ICommentDao>();
            imageUploadService = kernel.Get<IImageUploadService>();
        }



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

        private byte[] GetByteArray(int sizeInKb)
        {
            Random rnd = new Random();
            byte[] b = new byte[sizeInKb * 1024]; // convert kb to byte
            rnd.NextBytes(b);
            return b;
        }

        [TestMethod()]
        public void AddCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);

                List<string> tags = new List<String>();

                long imgId = imageUploadService.UploadImage(img, tags, "Paisaje");

                long commentId1 = commentService.AddComment(imgId, "Hola", userId);


                Comment comment1 = commentDao.Find(commentId1);

                Assert.IsTrue(comment1.content.Equals("Hola"));
     
            }
        }

        [TestMethod()]
        public void ShowCommentsTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);


                List<string> tags = new List<String>();

                long imgId = imageUploadService.UploadImage(img, tags, "Paisaje");

                long commentId1 = commentService.AddComment(imgId, "Commentary1", userId);

                Comment comment1 = commentDao.Find(commentId1);

                Assert.IsTrue(comment1.content.Equals("Commentary1"));

                var result = commentService.ShowComments(imgId, 0, 1);

                Assert.IsTrue(result.Count == 1);
            }
        }

        [TestMethod()]
        public void UpdateCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);


                List<string> tags = new List<String>();

                long imgId = imageUploadService.UploadImage(img, tags, "Paisaje");

                long commentId1 = commentService.AddComment(imgId, "Commentary1", userId);

                Comment comment1 = commentDao.Find(commentId1);

                Assert.IsTrue(comment1.content.Equals("Commentary1"));

                commentService.UpdateComment(commentId1, "ModifiedComentary1");

                comment1 = commentDao.Find(commentId1);

                Assert.IsTrue(comment1.content.Equals("ModifiedComentary1"));

            }
        }


        [TestMethod()]
        public void RemoveCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);


                List<string> tags = new List<String>();

                long imgId = imageUploadService.UploadImage(img, tags, "Paisaje");

                long commentId1 = commentService.AddComment(imgId, "Commentary1", userId);
                long commentId2 = commentService.AddComment(imgId, "Commentary2", userId);

                Comment comment1 = commentDao.Find(commentId1);
                Comment comment2 = commentDao.Find(commentId2);

                Assert.IsTrue(comment1.content.Equals("Commentary1"));
                Assert.IsTrue(comment2.content.Equals("Commentary2"));
                
                var result = commentService.ShowComments(imgId, 0, 2);

                Assert.IsTrue(result.Count == 2);

                commentService.RemoveComment(commentId1);
                result = commentService.ShowComments(imgId, 0, 2);
                Assert.IsTrue(result.Count == 1);

            }
        }
    }
}