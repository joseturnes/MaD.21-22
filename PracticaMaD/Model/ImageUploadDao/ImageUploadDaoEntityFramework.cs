using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao
{
    public class ImageUploadDaoEntityFramework : 
        GenericDaoEntityFramework<ImageUpload, Int64>, IImageUploadDao
    {
        int NUMBER_OF_COMMENTS = 3;
        int NUMBER_OF_IMAGES = 6;

        public ImageUploadDaoEntityFramework()
        {
        }

        public ImageUpload findImage(long imgId)
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

        public List<ImageUpload> FindByTitleOrDescription(string keyword,int startIndex, int count)
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
                 where a.usrId==userId
                 orderby a.uploadDate
                 select a).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<Comment> FindLastComments(long imgId, int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            ImageUpload image = findImage(imgId);

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

        public List<UserProfile> findUserProfiles(long imgId, int startIndex,
            int count)
        {

            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            List<UserProfile> result =
                (from u in images
                 where u.imgId == imgId
                 select u.UserProfile1).FirstOrDefault().Skip(startIndex).Take(count).ToList<UserProfile>();

            return result;

        }

        public int getNumberOfImages(long userId)
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
                 where a.categoryId==categoryId
                 orderby a.uploadDate
                 select a).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<ImageUpload> findRecentUploads(int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 orderby a.uploadDate
                 select a).Skip(startIndex).Take(count).ToList();
            result.Reverse();
            
            return result;
        }

        public int countRecentUploads()
        {
            return NUMBER_OF_IMAGES;
        }

        public List<Tag> FindImageTags(long imgId, int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where a.imgId == imgId
                 select a.Tag).FirstOrDefault().ToList();

            return result;
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
    }
}
