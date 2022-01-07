using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService
{
    public class ImageUploadConversor
    {
        public static ImageUploadDto ToImageUploadDto(ImageUpload image)
        {
            return new ImageUploadDto(image.uploadedImage, image.imgId, image.usrId, image.title, image.descriptions, image.uploadDate, image.likes, image.f, image.t, image.iso, image.wb);
        }
        public static List<ImageUploadDto> ToImageUploadDtos (List<ImageUpload> images)
        {
            List<ImageUploadDto> result = new List<ImageUploadDto>();

            for (int i = 0; i < images.Count; i++)
            {
                result.Add(ToImageUploadDto(images[i]));
            }

            return result;
        }

       

    }
}
