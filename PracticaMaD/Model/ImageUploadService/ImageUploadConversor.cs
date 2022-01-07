﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService
{
    public class ImageUploadConversor
    {
        public static List<ImageUploadDto> toImageUploadDtos (List<ImageUpload> images)
        {
            List<ImageUploadDto> result = new List<ImageUploadDto>();

            for (int i = 0; i < images.Count; i++)
            {
                result.Add(new ImageUploadDto(images[i].uploadedImage,images[i].imgId, images[i].usrId, images[i].title, images[i].descriptions, images[i].uploadDate, images[i].likes, 
                    images[i].f, images[i].t, images[i].iso, images[i].wb));
            }

            return result;
        }

        public static ImageUploadDto toImageUploadDto(ImageUpload image)
        {
            return new ImageUploadDto(image.uploadedImage, image.imgId, image.usrId, image.title, image.descriptions, image.uploadDate, image.likes, image.f, image.t, image.iso, image.wb);
        }

    }
}
