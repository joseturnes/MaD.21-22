using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService
{
    public class ImageUploadService : IImageUploadService
    {
        [Inject]
        public IImageUploadDao ImageUploadDao { private get; set; }

        public long UploadImage(ImageUploadDetails img)
        {
            ImageUpload image = new ImageUpload();
            image.title = img.title;
            image.descriptions = img.descriptions;
            image.uploadDate = DateTime.Now;
            image.f = img.f;
            image.t = img.t;
            image.iso = img.iso;
            image.wb = img.wb;
            image.category = img.category;
            ImageUploadDao.Create(image);

            return image.imgId;
        }
    }
}
