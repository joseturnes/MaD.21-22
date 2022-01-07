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

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);


                ImageUploadDetails img = new ImageUploadDetails("Titulo",image, userId,"Description",DateTime.Now,f1,f2, "ISO", "wb",10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2",image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2",20);


                long id = imageUploadService.UploadImage(img, tags,"Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags, "Retrato");
                ImageUpload result = imageUploadDao.Find(id);

                Assert.IsTrue(result.imgId == id);

                // Commment

                long commId= commentService.AddComment(id,"commentary1",userId);
                commentService.AddComment(id, "commentary2", userId);

                List<CommentDto> comments = imageUploadService.searchComments(id, 0, 10);
                CommentDto comments1 = new CommentDto(commId,"commentary1",userId,loginName,id,DateTime.Now);

                Assert.IsTrue(2==imageUploadService.CountComments(id));
                Assert.IsNotNull(comments);
                Assert.IsTrue(2 == comments.Count);


            }
        }

        [TestMethod()]
        public void UpdateImageTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);


                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2", image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2", 20);


                long id = imageUploadService.UploadImage(img, tags, "Paisaje");

                ImageUpload result = imageUploadDao.Find(id);




            }
        }

        [TestMethod()]
        public void RemoveImageTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");
    
                ImageUpload result = imageUploadDao.Find(id);

                Assert.IsNotNull(result);

                imageUploadDao.Remove(id);

                Assert.IsNotNull(result);


            }
        }

        [TestMethod()]
        public void LikedImageTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");

                ImageUpload result = imageUploadDao.Find(id);

                imageUploadService.LikedImage(id, userId);

                result = imageUploadDao.Find(id);

                long numLikes = result.likes;

                Assert.AreEqual(1, numLikes);



            }
        }

        [TestMethod()]
        public void UnLikedImageTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");

                ImageUpload result = imageUploadDao.Find(id);

                imageUploadService.LikedImage(id, userId);

                result = imageUploadDao.Find(id);

                long numLikes = result.likes;

                Assert.AreEqual(1, numLikes);

                imageUploadService.UnlikeImage(id, userId);

                result = imageUploadDao.Find(id);

                numLikes = result.likes;

                Assert.AreEqual(0, numLikes);



            }
        }


        [TestMethod()]
        public void FindByKeywordAndCategoryTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2", image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2", 20);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags, "Retrato");
                ImageUploadDto result = ImageUploadConversor.toImageUploadDto(imageUploadDao.Find(id));


                Assert.IsTrue(imageUploadService.FindByKeywordAndCategory("des", 3, 0, 1000).Contains(result));
                Assert.IsTrue(imageUploadService.FindByKeywordAndCategory("des", 0, 0, 1000).Contains(result));
                Assert.IsTrue(imageUploadService.FindByKeywordAndCategory("des", 0, 0, 1000).Count() == 2);
                Assert.IsTrue(imageUploadService.FindByKeywordAndCategory("sadasda", 0, 0, 1000).Count() == 0);
                List<ImageUploadDto> images = imageUploadService.FindByKeywordAndCategory("des", 0, 0, 1000);
                Assert.IsTrue(images.Count() == imageUploadService.countSearchKeywords("des", 0));

            }
        }



        [TestMethod()]
        public void RecentUploadsTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2", image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2", 20);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags, "Retrato");
                ImageUpload result = imageUploadDao.Find(id);


                List<ImageUploadDto> recentImages = new List<ImageUploadDto>();

                recentImages.Add(imageUploadService.findImage(id));
                recentImages.Add(imageUploadService.findImage(id2));


                Assert.AreEqual(recentImages.Count, imageUploadService.recentUploads(userId, 0, 6).Count);
                //Assert.AreEqual(2, imageUploadService.countRecentUploads());


            }
        }


        [TestMethod()]
        public void FindImageTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2", image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2", 20);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags, "Retrato");
                
                ImageUploadDto result = ImageUploadConversor.toImageUploadDto( imageUploadDao.Find(id));


                Assert.AreEqual(result, imageUploadService.findImage(id));


            }
        }


        [TestMethod()]
        public void IsLikedTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                UserProfileDetails user1 = new UserProfileDetails("user2", firstName, lastName, email, language, country);
                long user1Id = userService.RegisterUser("user2", "1234", user);
                

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2", image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2", 20);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags, "Retrato");

                ImageUpload result = imageUploadDao.Find(id);

                imageUploadService.LikedImage(id, user1Id);

                Assert.AreEqual(true, imageUploadService.isLiked(id, user1Id));


            }
        }


        [TestMethod()]
        public void GetNumberOfImagesTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                UserProfileDetails user1 = new UserProfileDetails("user2", firstName, lastName, email, language, country);
                long user1Id = userService.RegisterUser("user2", "1234", user);


                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2", image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2", 20);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags, "Retrato");

                ImageUpload result = imageUploadDao.Find(id);

                long numberOfImages = imageUploadService.getNumberOfImages(userId);

                Assert.AreEqual(2, numberOfImages);


            }
        }

        
        [TestMethod()]
        public void FindRecentUploadsTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2", image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2", 20);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags, "Retrato");
                ImageUpload result = imageUploadDao.Find(id);


                List<ImageUploadDto> recentImages = new List<ImageUploadDto>();

                recentImages.Add(imageUploadService.findImage(id));
                recentImages.Add(imageUploadService.findImage(id2));

                List<ImageUploadDto> images = imageUploadService.FindRecentUploads();

                Assert.AreEqual(recentImages.Count, images.Count);


            }
        } 
        

        [TestMethod()]
        public void FindImagesTagsTest()
        {
            using (var scope = new TransactionScope())
            {
                initializeKernel();
                float f1 = 1;
                float f2 = 1;

                long tagId = tagDao.CreateTag("Luces").tagId;
                long tagId2 = tagDao.CreateTag("Vigo").tagId;

                List<string> tags = new List<String>();
                tags.Add("Coruña");
                tags.Add("Navidades boas");
                tags.Add("Luces");
                tags.Add("Luces2");
                tags.Add("Luces3");
                tags.Add("Vigo");

                byte[] image = GetByteArray(512);

                UserProfileDetails user = new UserProfileDetails(loginName, firstName, lastName, email, language, country);
                long userId = userService.RegisterUser(loginName, clearPassword, user);

                ImageUploadDetails img = new ImageUploadDetails("Titulo", image, userId, "Description", DateTime.Now, f1, f2, "ISO", "wb", 10);
                ImageUploadDetails img2 = new ImageUploadDetails("Titulo2", image, userId, "Description2", DateTime.Now, f1, f2, "ISO2", "wb2", 20);

                long id = imageUploadService.UploadImage(img, tags, "Paisaje");
                long id2 = imageUploadService.UploadImage(img2, tags, "Retrato");
                ImageUpload result = imageUploadDao.Find(id);

                Tag tag = tagDao.CreateTag("Coruña 2");

                imageUploadService.addTag(tag, id);

                List<Tag> imageTags = new List<Tag>();
                imageTags.Add(tagDao.FindByName("Coruña"));
                imageTags.Add(tagDao.FindByName("Coruña 2"));
                imageTags.Add(tagDao.FindByName("Navidades boas"));
                imageTags.Add(tagDao.FindByName("Luces"));
                imageTags.Add(tagDao.FindByName("Luces2"));
                imageTags.Add(tagDao.FindByName("Luces3"));
                imageTags.Add(tagDao.FindByName("Vigo"));


                Assert.AreEqual(imageTags.Count, imageUploadService.FindImageTags(id, 0, 7).Count);
                Assert.AreEqual(7, imageUploadService.CountImageTags(id));
            }
        }


    }
}