using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao
{
    public class ImageUploadDaoEntityFramework :
        GenericDaoEntityFramework<ImageUpload, Int64>, IImageUploadDao
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }


        int NUMBER_OF_IMAGES = 12;

        public ImageUploadDaoEntityFramework()
        {
        }

        public ImageUpload FindImage(long imgId)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            ImageUpload result =
                (from a in images
                 where a.imgId == imgId
                 select a).FirstOrDefault();

            return result;
        }

        public int CountComments(long imgId)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where a.imgId == imgId
                 select a.Comment).FirstOrDefault().ToList<Comment>();

            return result.Count();
        }

        public List<ImageUpload> FindByTitleOrDescription(string keyword, int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();
            keyword = keyword.ToLower();

            var result =
                (from a in images
                 where (a.title.ToLower().Contains(keyword) || a.descriptions.ToLower().Contains(keyword))
                 orderby a.uploadDate
                 select a).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<ImageUpload> FindByUserIdOrderByDateAsc(int userId, int startIndex,
            int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where a.usrId == userId
                 orderby a.uploadDate
                 select a).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<Comment> FindLastComments(long imgId, int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            ImageUpload image = FindImage(imgId);

            List<Comment> result = image.Comment.ToList();
            result.Reverse();
            return result;
        }

        public List<ImageUpload> FindLastPublications(long userId, int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where a.usrId == userId
                 orderby a.uploadDate descending
                 select a).Skip(startIndex).Take(count).ToList();
            return result;
        }

        public List<UserProfile> FindUserProfiles(long imgId, int startIndex,
            int count)
        {

            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            List<UserProfile> result =
                (from u in images
                 where u.imgId == imgId
                 select u.UserProfile1).FirstOrDefault().Skip(startIndex).Take(count).ToList<UserProfile>();

            return result;

        }

        public int GetNumberOfImages(long userId)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            List<ImageUpload> result =
                (from u in images
                 where u.UserProfile.usrId == userId
                 select u).ToList<ImageUpload>();

            return result.Count;
        }

        public List<ImageUpload> FindByCategory(long categoryId, int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where a.categoryId == categoryId
                 orderby a.uploadDate
                 select a).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<ImageUpload> FindRecentUploads()
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 orderby a.uploadDate
                 select a).ToList();
            result.Reverse();

            return result;
        }

        public int CountRecentUploads()
        {
            return NUMBER_OF_IMAGES;
        }

        public List<Tag> FindImageTags(long imgId, int startIndex, int count)
        {
            ImageUpload image = FindImage(imgId);

            return image.Tag.ToList();
        }

        public int CountImageTags(long imgId)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where a.imgId == imgId
                 select a.Tag).FirstOrDefault().ToList();

            return result.Count;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public bool IsLiked(long imgId, long usrId)
        {
            ImageUpload image = Find(imgId);
            UserProfile user = UserProfileDao.Find(usrId);

            if (image.UserProfile1.Contains(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void LikeImage(long imgId, long usrId)
        {
            ImageUpload img = Find(imgId);
            UserProfile user = UserProfileDao.Find(usrId);

            if (!(img.UserProfile1.Contains(user) || user.ImageUpload1.Contains(img)))
            {
                img.likes++;
                img.UserProfile1.Add(user);
                Update(img);
                user.ImageUpload1.Add(img);
                UserProfileDao.Update(user);
            }

        }

        /// <exception cref="InstanceNotFoundException"/>
        public void UnlikeImage(long imgId, long usrId)
        {
            ImageUpload img = Find(imgId);
            UserProfile user = UserProfileDao.Find(usrId);

            if ((img.UserProfile1.Contains(user) || user.ImageUpload1.Contains(img)))
            {
                img.likes--;
                img.UserProfile1.Remove(user);
                Update(img);
                user.ImageUpload1.Remove(img);
                UserProfileDao.Update(user);
            }
        }
    }
}
