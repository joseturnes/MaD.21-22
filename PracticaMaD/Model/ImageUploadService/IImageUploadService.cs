using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService
{
    public interface IImageUploadService
    {
        [Inject]
        IImageUploadDao ImageUploadDao { set; }
        [Inject]
        ITagDao TagDao { set; }

        /// <summary>
        /// Upload an image.
        /// </summary>
        ///<param name="ImageUploadDetails"> The image params. </param>
        long UploadImage(ImageUploadDetails img, List<string> tags);

        /// <summary>
        /// Search images by keywords.
        /// </summary>
        ///<param name="keywords"> The search params. </param>
        List<ImageUpload> SearchByKeywords(string keywords, int startIndex, int count);
    }
}
