using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService.Exceptions;
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
        [Inject]
        public ITagDao TagDao { private get; set; }


        public long UploadImage(ImageUploadDetails img, List<string> tags)
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
            if (!(tags==null))
            {
                for (int i = 0; i < tags.Count; i++)
                {
                    try
                    {
                        Tag tag = new Tag();
                        tag.tagname = tags[i];
                        TagDao.Create(tag);
                    }
                    catch (AlreadyCreatedException)
                    {
                    }
                    finally
                    {
                        Tag tag = TagDao.FindByName(tags[i]);
                        if (!image.Tag.Contains(tag))
                        {
                            image.Tag.Add(tag);
                            Tag tag1 = TagDao.FindByName(tags[i]);
                            tag1.ImageUpload.Add(image);
                        }
                    }

                }
            }
            ImageUploadDao.Create(image);

            return image.imgId;
        }

        public List<ImageUpload> SearchByKeywords(string keywords, int startIndex, int count)
        {
            return ImageUploadDao.FindByTitleOrDescriptionOrCategory(keywords,startIndex,count+1);
        }
    }
}
