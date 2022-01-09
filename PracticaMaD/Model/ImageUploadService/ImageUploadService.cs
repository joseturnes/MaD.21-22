using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService
{
    public class ImageUploadService : IImageUploadService
    {
        [Inject]
        public IImageUploadDao ImageUploadDao { private get; set; }
        [Inject]
        public ITagDao TagDao { private get; set; }
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }
        [Inject]
        public ICategoryDao CategoryDao { private get; set; }
        [Inject]
        public ITagService TagService { private get; set; }

        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public long UploadImage(ImageUploadDetails img, List<string> tags, string category)
        {
            ImageUpload image = new ImageUpload();
            image.uploadedImage = img.uploadedImage;
            image.usrId = img.usrId;
            image.title = img.title;
            image.descriptions = img.descriptions;
            image.uploadDate = DateTime.Now;
            image.f = img.f;
            image.t = img.t;
            image.iso = img.iso;
            image.wb = img.wb;
            Category categoryObj = new Category();


            categoryObj = CategoryDao.FindByName(category);

            categoryObj.ImageUpload.Add(image);
            CategoryDao.Update(categoryObj);
            image.categoryId = categoryObj.categoryId;

            TagService.UpdateTags(image.imgId, tags);

            return image.imgId;
        }


        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateImage(long imgId, ImageUploadDetails imageDetails)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);

            if (img.Equals(null))
            {
                throw new InstanceNotFoundException(imgId, typeof(long).FullName);
            }

            img.title = imageDetails.title;
            img.descriptions = imageDetails.descriptions;
            img.uploadDate = imageDetails.uploadDate;
            img.f = imageDetails.f;
            img.t = imageDetails.t;
            img.iso = imageDetails.iso;
            img.likes = imageDetails.likes;
            img.wb = imageDetails.wb;

            ImageUploadDao.Update(img);
        }

        [Transactional]
        public List<ImageUploadDto> FindByKeywordAndCategory(string keywords, long categoryId, int startIndex, int count)
        {
            List<ImageUpload> result = new List<ImageUpload>();
            List<ImageUpload> resultaux = new List<ImageUpload>();

            if (categoryId == 0)
            {
                result = ImageUploadDao.FindByTitleOrDescription(keywords, startIndex, count);
            }
            else
            {
                resultaux = ImageUploadDao.FindByCategory(categoryId, startIndex, count);
                foreach (var res in resultaux)
                {
                    if (string.IsNullOrEmpty(keywords) || ((res.descriptions.ToLower().Contains(keywords.ToLower())) || (res.title.ToLower().Contains(keywords.ToLower()))))
                    {
                        result.Add(res);
                    }
                }
            }
            return ImageUploadConversor.ToImageUploadDtos(result);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void RemoveImage(long imgId)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);
            ImageUploadDao.Remove(imgId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void LikeImage(long imgId, long userId)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);
            UserProfile user = UserProfileDao.Find(userId);

            if (img.Equals(null))
            {
                throw new InstanceNotFoundException(imgId, typeof(long).FullName);
            }

            if (user.Equals(null))
            {
                throw new InstanceNotFoundException(userId, typeof(long).FullName);
            }

            ImageUploadDao.LikeImage(imgId, userId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UnlikeImage(long imgId, long userId)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);
            UserProfile user = UserProfileDao.Find(userId);

            if (img.Equals(null))
            {
                throw new InstanceNotFoundException(imgId, typeof(long).FullName);
            }

            if (user.Equals(null))
            {
                throw new InstanceNotFoundException(userId, typeof(long).FullName);
            }

            ImageUploadDao.UnlikeImage(imgId, userId);
        }


        [Transactional]
        public int CountComments(long imgId)
        {
            return ImageUploadDao.CountComments(imgId);
        }

        [Transactional]
        public int GetNumberOfImages(long userId)
        {
            return ImageUploadDao.GetNumberOfImages(userId);
        }

        [Transactional]
        public List<ImageUploadDto> RecentUploads(long userId, int startIndex, int count)
        {
            List<ImageUploadDto> result = ImageUploadConversor.ToImageUploadDtos(ImageUploadDao.FindLastPublications(userId, startIndex, count + 1));
            return result;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public ImageUploadDto FindImage(long imgId)
        {
            return ImageUploadConversor.ToImageUploadDto(ImageUploadDao.FindImage(imgId));
        }

        [Transactional]
        public List<CommentDto> SearchComments(long imgId, int startIndex, int count)
        {
            List<Comment> comments = ImageUploadDao.FindLastComments(imgId, startIndex, count);
            List<CommentDto> result = new List<CommentDto>();

            for (int i = 0; i < comments.Count; i++)
            {
                string userName = UserProfileDao.FindById(comments[i].usrId).loginName;
                result.Add(new CommentDto(comments[i].commentId, comments[i].content, comments[i].usrId, userName, comments[i].imgId, comments[i].comDate));
            }


            return result;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public bool IsLiked(long imgId, long userId)
        {
            return ImageUploadDao.IsLiked(imgId, userId);
        }

        [Transactional]
        public int CountSearchKeywords(string keywords, long categoryId)
        {
            return FindByKeywordAndCategory(keywords, categoryId, 0, 10000).Count;
        }

        public List<ImageUploadDto> FindRecentUploads()
        {
            List<ImageUploadDto> result = ImageUploadConversor.ToImageUploadDtos(ImageUploadDao.FindRecentUploads());
            return result;
        }

        public int CountRecentUploads()
        {
            return ImageUploadDao.CountRecentUploads();
        }

        /// <exception cref="InstanceNotFoundException"/>
        public List<Tag> FindImageTags(long imgId, int startIndex, int count)
        {
            return ImageUploadDao.FindImageTags(imgId, startIndex, count);
        }

        public int CountImageTags(long imgId)
        {
            return ImageUploadDao.CountImageTags(imgId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void AddTag(Tag tag, long imgId)
        {
            ImageUpload image = ImageUploadDao.Find(imgId);
            List<Tag> tags = image.Tag.ToList();

            if (image != null)
            {
                tags.Add(tag);
                image.Tag = tags;
                ImageUploadDao.Update(image);
            }
        }
    }
}
