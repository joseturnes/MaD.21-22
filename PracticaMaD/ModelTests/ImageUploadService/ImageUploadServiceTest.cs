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

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService.Test
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
        public void UploadImageTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagService.CreateTag("Luces");

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);


                ImageUploadDetails img = new ImageUploadDetails("Titulo",image, userId,"Description",DateTime.Now,f1,f2, "ISO", "wb",10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2",image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2",20);

                

                long id = imageUploadService.UploadImage(img, tags,"Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags, "Retrato");
                ImageUpload result = imageUploadDao.Find(id);

                Assert.IsTrue(imageUploadService.FindByKeywordAndCategory("des", 3, 0, 1000).Contains(result));
                Assert.IsTrue(imageUploadService.FindByKeywordAndCategory("des", 0, 0, 1000).Contains(result));
                Assert.IsTrue(imageUploadService.FindByKeywordAndCategory("des", 0, 0, 1000).Count()==2);
                Assert.IsTrue(imageUploadService.FindByKeywordAndCategory("sadasda", 0, 0, 1000).Count() == 0);
                List<ImageUpload> images = imageUploadService.FindByKeywordAndCategory("des", 0, 0, 1000);
                Assert.IsTrue(images.Count() == imageUploadService.countSearchKeywords("des",0));

                Assert.IsTrue(result.imgId == id);
                Assert.IsTrue(result.Tag.Contains(tagDao.Find(tagId)));
                Assert.IsTrue(tagDao.Find(tagId).ImageUpload.Contains(result));
                Assert.IsTrue(result.Category.categoryName.Equals("Paisaje"));
                Assert.AreEqual(result.Tag.Count, 5);

                long commId= commentService.AddComment(id,"commentary1",userId);
                commentService.AddComment(id, "commentary2", userId);

                List<CommentDto> comments = imageUploadService.searchComments(id, 0, 10);
                CommentDto comments1 = new CommentDto(commId,"commentary1",userId,loginName,id,DateTime.Now);

                Assert.IsTrue(2==imageUploadService.CountComments(id));
                Assert.IsNotNull(comments);
                Assert.IsTrue(2 == comments.Count);
                //Assert.IsTrue(comments.Contains(comments1));




            }
        }

        //[TestMethod()]
        //public void FindAllTagsTest()
        //{
        //    using (var scope = new TransactionScope())
        //    {
        //        initializeKernel();
                
        //        long tagId1 = tagService.CreateTag("Luces");
        //        long tagId2 = tagService.CreateTag("Coruña");
        //        long tagId3 = tagService.CreateTag("Navidad");

        //        Tag tag1 = tagDao.Find(tagId1);
        //        Tag tag2 = tagDao.Find(tagId2);
        //        Tag tag3 = tagDao.Find(tagId3);

        //        List<Tag> tags = tagService.GetAllTags();

        //        Assert.IsTrue(tags.Contains(tag1)&& tags.Contains(tag2)&& tags.Contains(tag3));
            

        //    }
        //}

        //[TestMethod()]
        //public void SearchUploadImagesTest()
        //{
        //    using (var scope = new TransactionScope())
        //    {
        //        initializeKernel();
        //        float f1 = 1;
        //        float f2 = 1;

        //        ImageUploadDetails img = new ImageUploadDetails("Arboles", "Description arboles",
        //            DateTime.Now, f1, f2, "ISO", "wb");
        //        ImageUploadDetails img2 = new ImageUploadDetails("Cascadas", "Description cascadas",
        //            DateTime.Now, f1, f2, "ISO", "wb");
        //        ImageUploadDetails img3 = new ImageUploadDetails("Ciudades", "Description ciudades",
        //            DateTime.Now, f1, f2, "ISO", "wb");
        //        ImageUploadDetails img4 = new ImageUploadDetails("Arboles2", "Description arboles",
        //            DateTime.Now, f1, f2, "ISO", "wb");

        //        long id = imageUploadService.UploadImage(img, null,"paisaje");
        //        long id2 = imageUploadService.UploadImage(img2, null, "paisaje");
        //        long id3 = imageUploadService.UploadImage(img3, null, "paisaje");
        //        long id4 = imageUploadService.UploadImage(img4, null, "paisaje");
        //        ImageUpload result = imageUploadDao.Find(id);
        //        ImageUpload result2 = imageUploadDao.Find(id2);
        //        ImageUpload result3 = imageUploadDao.Find(id3);
        //        ImageUpload result4 = imageUploadDao.Find(id4);

        //        List<ImageUpload> images = imageUploadDao.FindByTitleOrDescriptionOrCategory("Arboles", 0, 10);
        //        Assert.IsTrue(images.Contains(result) && images.Contains(result4));
        //        List<ImageUpload> images2 = imageUploadDao.FindByTitleOrDescriptionOrCategory("Description", 0, 10);
        //        Assert.IsTrue(images2.Contains(result) && images2.Contains(result2) && images2.Contains(result3) && images2.Contains(result4));
        //    }
        //}
    }
}