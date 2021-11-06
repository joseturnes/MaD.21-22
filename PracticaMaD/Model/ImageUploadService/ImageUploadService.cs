using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService
{
    public class ImageUploadService : IImageUploadService
    {
        [Inject]
        public IImageUploadDao ImageUploadDao { private get; set; }
        [Inject]
        public ITagDao TagDao { private get; set; }
        [Inject]
        public ICategoryDao CategoryDao { private get; set; }
        [Inject]
        public ITagService TagService { private get; set; }

        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public long UploadImage(ImageUploadDetails img, List<string> tags, string category)
        {
            ImageUpload image = new ImageUpload();
            image.title = img.title;
            image.descriptions = img.descriptions;
            image.uploadDate = DateTime.Now;
            image.f = img.f;
            image.t = img.t;
            image.iso = img.iso;
            image.wb = img.wb;
            Category categoryObj = new Category();

            if (!(tags==null))
            {
                for (int i = 0; i < tags.Count; i++)
                {
                    try
                    {
                        TagService.CreateTag(tags[i]);
                    }
                    catch (AlreadyCreatedException)
                    {
                    }
                    finally
                    {
                        Tag tag = TagDao.FindByName(tags[i]);
                        if (!(image.Tag.Contains(tag) && tag.ImageUpload.Contains(image)))
                        {
                            image.Tag.Add(tag);
                            //tag.ImageUpload.Add(image);
                            //TagDao.Update(tag);
                        }
                    }

                }
            }

            categoryObj = CategoryDao.FindByName(category);

            categoryObj.ImageUpload.Add(image);
            CategoryDao.Update(categoryObj);
            image.categoryId = categoryObj.categoryId;

            ImageUploadDao.Create(image);

            return image.imgId;
        }

        [Transactional]
        public List<ImageUpload> SearchByKeywords(string keywords, int startIndex, int count)
        {
            return ImageUploadDao.FindByTitleOrDescriptionOrCategory(keywords,startIndex,count+1);
        }
    }
}
