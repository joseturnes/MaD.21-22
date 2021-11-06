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

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService.Test
{
    [TestClass()]
    public class ImageUploadServiceTest
    {
        private static IKernel kernel;
        private static IImageUploadService imageUploadService;
        private static IImageUploadDao imageUploadDao;


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
        public void UploadImageTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                imageUploadService = kernel.Get<IImageUploadService>();
                imageUploadDao = kernel.Get<IImageUploadDao>();
                float f1 = 1;
                float f2 = 1;

                ImageUploadDetails img = new ImageUploadDetails("Titulo", "Description",
                    DateTime.Now,f1,f2, "ISO", "wb","category");

                long id = imageUploadService.UploadImage(img);
                ImageUpload result = imageUploadDao.Find(id);
                Assert.IsTrue(result.imgId == id);
            }
        }

        [TestMethod()]
        public void SearchUploadImagesTest()
        {
            using (var scope = new TransactionScope())
            {
                kernel = TestManager.ConfigureNInjectKernel();
                imageUploadService = kernel.Get<IImageUploadService>();
                imageUploadDao = kernel.Get<IImageUploadDao>();
                float f1 = 1;
                float f2 = 1;

                ImageUploadDetails img = new ImageUploadDetails("Arboles", "Description arboles",
                    DateTime.Now, f1, f2, "ISO", "wb", "arboles category");
                ImageUploadDetails img2 = new ImageUploadDetails("Cascadas", "Description cascadas",
                    DateTime.Now, f1, f2, "ISO", "wb", "cascadas category");
                ImageUploadDetails img3 = new ImageUploadDetails("Ciudades", "Description ciudades",
                    DateTime.Now, f1, f2, "ISO", "wb", "ciudades category");
                ImageUploadDetails img4 = new ImageUploadDetails("Arboles2", "Description arboles",
                    DateTime.Now, f1, f2, "ISO", "wb", "arboles category");

                long id = imageUploadService.UploadImage(img);
                long id2 = imageUploadService.UploadImage(img2);
                long id3 = imageUploadService.UploadImage(img3);
                long id4 = imageUploadService.UploadImage(img4);
                ImageUpload result = imageUploadDao.Find(id);
                ImageUpload result2 = imageUploadDao.Find(id2);
                ImageUpload result3 = imageUploadDao.Find(id3);
                ImageUpload result4 = imageUploadDao.Find(id4);

                List<ImageUpload> images = imageUploadDao.FindByTitleOrDescriptionOrCategory("Arboles", 0, 10);
                Assert.IsTrue(images.Contains(result) && images.Contains(result4));
                List<ImageUpload> images2 = imageUploadDao.FindByTitleOrDescriptionOrCategory("Description", 0, 10);
                Assert.IsTrue(images2.Contains(result) && images2.Contains(result2) && images2.Contains(result3) && images2.Contains(result4));
            }
        }
    }
}