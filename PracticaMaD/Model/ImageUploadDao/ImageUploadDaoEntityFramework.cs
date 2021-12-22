using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public long CountComments(long imgId, int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where a.imgId == imgId
                 select a.Comment).Skip(startIndex).Take(count).ToList();

            return result.Count;
        }

        public List<ImageUpload> FindByTitleOrDescriptionOrCategory(string keyword,int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where (a.title.Contains(keyword) || a.descriptions.Contains(keyword))
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

            var comments =
                (from a in images
                 where a.imgId == imgId
                 select a.Comment).FirstOrDefault().Skip(startIndex).Take(NUMBER_OF_COMMENTS).ToList();

            return comments;
        }

        public List<ImageUpload> FindLastPublications(long userId, int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where a.usrId == userId
                 orderby a.uploadDate
                 select a).Skip(startIndex).Take(NUMBER_OF_IMAGES).ToList();

            return result;
        }

        public List<UserProfile> findUserProfiles(long imgId)
        {
            throw new NotImplementedException();
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
    }
}
