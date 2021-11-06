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
    }
}