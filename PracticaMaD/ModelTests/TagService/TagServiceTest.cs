using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.ModelTests;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService.Test
{
    [TestClass()]
    public class ImageUploadServiceTest
    {
        private static IKernel kernel;
        private static IImageUploadService imageUploadService;
        private static IImageUploadDao imageUploadDao;
        private static ITagService tagService;
        private static ITagDao tagDao;
        private static ICategoryDao categoryDao;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static ICommentService commentService;
        private static ICommentDao commentDao;
        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";
        private const string country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        private TransactionScope transaction;

        private TestContext testContextInstance;

        private void initializeKernel()
        {
            kernel = TestManager.ConfigureNInjectKernel();
            imageUploadService = kernel.Get<IImageUploadService>();
            imageUploadDao = kernel.Get<IImageUploadDao>();
            tagService = kernel.Get<ITagService>();
            tagDao = kernel.Get<ITagDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            userService = kernel.Get<IUserService>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            commentService = kernel.Get<ICommentService>();
            commentDao = kernel.Get<ICommentDao>();
        }


        ///// <summary>
        ///// Gets or sets the test context which provides information about and functionality for the
        ///// current test run.
        ///// </summary>
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
        public void GetAllTagsTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                var tags = tagService.GetAllTags();

                Assert.IsTrue(tags.Contains(tagDao.Find(tagId)));
                Assert.IsTrue(tags.Contains(tagDao.Find(tagId2)));

                long tagId3 = tagDao.CreateTag("Dinosaurio").tagId;
                long tagId4 = tagDao.CreateTag("Noria").tagId;

                tags = tagService.GetAllTags();

                Assert.IsTrue(tags.Contains(tagDao.Find(tagId3)));
                Assert.IsTrue(tags.Contains(tagDao.Find(tagId4)));

            }
        }

        [TestMethod()]
        public void FindMostUsedTagsTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                byte[] image = GetByteArray(512);

                long tagId = tagDao.CreateTag("Navidades boas").tagId;
                long tagId2 = tagDao.CreateTag("Coruña").tagId;
                long tagId3 = tagDao.CreateTag("Luces").tagId;

                List<string> tags1 = new List<String>();
                tags1.Add("Navidades boas");

                List<string> tags2 = new List<String>();
                tags2.Add("Coruña");
                tags2.Add("Navidades boas");
                tags2.Add("Luces");

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);


                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2", image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2", 20);


                long id = imageUploadService.UploadImage(img, tags1, "Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags2, "Retrato");
                ImageUpload result = imageUploadDao.Find(id);

                var imageTag = tagService.FindMostUsedTags(0, 5);

                Assert.IsTrue(imageTag.Contains(tagDao.Find(tagId)));
                Assert.IsTrue(imageTag.Contains(tagDao.Find(tagId2)));
                Assert.IsTrue(imageTag.Contains(tagDao.Find(tagId3)));

            }
        }

        [TestMethod()]
        public void FindImagesByTagIdTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                byte[] image = GetByteArray(512);

                long tagId = tagDao.CreateTag("Navidades boas").tagId;
                long tagId2 = tagDao.CreateTag("Coruña").tagId;
                long tagId3 = tagDao.CreateTag("Luces").tagId;


                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);


                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2", image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2", 20);


                long id = imageUploadService.UploadImage(img, tags, "Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags, "Retrato");
                ImageUpload result = imageUploadDao.Find(id);
                ImageUpload result2 = imageUploadDao.Find(id2);

                var imageTag = tagService.FingImagesByTagId(tagId, 0, 3);

                Assert.IsTrue(imageTag.Contains(result));
                Assert.IsTrue(imageTag.Contains(result2));

            }
        }

        [TestMethod()]
        public void UpdateTagTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                byte[] image = GetByteArray(512);

                long tagId = tagDao.CreateTag("Navidades boas").tagId;
                long tagId2 = tagDao.CreateTag("Coruña").tagId;
                long tagId3 = tagDao.CreateTag("Luces").tagId;


                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");
                

                List<string> updateTags = new List<String>();
                updateTags.Add("Coruña");
                updateTags.Add("Luces");

                tagService.UpdateTags(id, updateTags);

                ImageUpload result = imageUploadDao.Find(id);

                Assert.IsTrue(result.Tag.Contains(tagDao.Find(tagId2)));
                Assert.IsTrue(result.Tag.Contains(tagDao.Find(tagId3)));



            }
        }
    }
}
