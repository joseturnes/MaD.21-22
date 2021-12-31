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
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using System.Data;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;

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
            image.uploadDate = img.uploadDate;
            image.f = img.f;
            image.t = img.t;
            image.iso = img.iso;
            image.wb = img.wb;
            Category categoryObj = new Category();


            categoryObj = CategoryDao.FindByName(category);

            categoryObj.ImageUpload.Add(image);
            CategoryDao.Update(categoryObj);
            image.categoryId = categoryObj.categoryId;

            TagService.updateTags(image.imgId,tags);

            return image.imgId;
        }

        

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
        public List<ImageUpload> FindByKeywordAndCategory(string keywords, long categoryId, int startIndex, int count)
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
            return result;
        }

        [Transactional]
        /// <exception cref="InstanceNotFoundException"/>
        public void RemoveImage(long imgId)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);
            ImageUploadDao.Remove(imgId);
        }

        [Transactional]
        public void LikedImage(long imgId, long userId)
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

            if (!(img.UserProfile1.Contains(user) || user.ImageUpload1.Contains(img)))
            {
                img.likes++;
                img.UserProfile1.Add(user);
                ImageUploadDao.Update(img);
                user.ImageUpload1.Add(img);
                UserProfileDao.Update(user);
            }
        }

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

            if ((img.UserProfile1.Contains(user) || user.ImageUpload1.Contains(img)))
            {
                img.likes--;
                img.UserProfile1.Remove(user);
                ImageUploadDao.Update(img);
                user.ImageUpload1.Remove(img);
                UserProfileDao.Update(user);
            }
        }


        [Transactional]
        public int CountComments(long imgId)
        {
            return ImageUploadDao.CountComments(imgId);
        }

        [Transactional]
        List<UserProfile> FollowerList(long userId, int startIndex, int count)
        {
            return UserProfileDao.FindFollowers(userId, startIndex, count);
        }

        [Transactional]
        List<UserProfile> ListOfFollows(long userId, int startIndex, int count)
        {
            return UserProfileDao.FindFollows(userId, startIndex, count);
        }

        [Transactional]
        public int getNumberOfImages(long userId)
        {
            return ImageUploadDao.getNumberOfImages(userId);
        }

        [Transactional]
        public List<ImageUploadDto> recentUploads(long userId, int startIndex, int count)
        {
            return ImageUploadConversor.toImageUploadDtos(ImageUploadDao.FindLastPublications(userId, startIndex, count + 1));
        }

        [Transactional]
        public ImageUpload findImage(long imgId)
        {
            return ImageUploadDao.findImage(imgId);
        }

        [Transactional]
        public List<CommentDto> searchComments(long imgId, int startIndex, int count)
        {
            List<Comment> comments = ImageUploadDao.FindLastComments(imgId, startIndex, count);
            List<CommentDto> result = new List<CommentDto>();

            for (int i = 0; i < comments.Count; i++)
            {
                string userName = UserProfileDao.FindById(comments[i].usrId).loginName;
                result.Add(new CommentDto(comments[i].commentId,comments[i].content, comments[i].usrId, userName, comments[i].imgId, comments[i].comDate));
            }


            return result;
        }

        [Transactional]
        public bool isLiked(long imgId, long userId)
        {
            ImageUpload image = ImageUploadDao.Find(imgId);
            UserProfile user = UserProfileDao.Find(userId);

            if (image.UserProfile1.Contains(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Transactional]
        public int countSearchKeywords(string keywords, long categoryId)
        {
            return FindByKeywordAndCategory(keywords, categoryId, 0, 10000).Count;
        }

        public List<ImageUpload> FindRecentUploads(int startIndex, int count)
        {
            return ImageUploadDao.findRecentUploads(startIndex,count);
        }

        public int countRecentUploads()
        {
            return ImageUploadDao.countRecentUploads();
        }

        public List<Tag> FindImageTags(long imgId, int startIndex, int count)
        {
            return ImageUploadDao.FindImageTags(imgId,startIndex,count);
        }

        public int CountImageTags(long imgId)
        {
            return ImageUploadDao.CountImageTags(imgId);
        }

        public void addTag(Tag tag, long imgId)
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
