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

        public long CountComments(long imgId)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where a.imgId == imgId
                 select a.Comment).FirstOrDefault().ToList<Comment>();

            return result.Count();
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

            ImageUpload image = findImage(imgId);

            return image.Comment.ToList();
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

        public int getNumberOfImages(long userId)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            List<ImageUpload> result =
                (from u in images
                 where u.UserProfile.usrId == userId
                 select u).ToList<ImageUpload>();

            return result.Count;
        }

        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();
            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = Record.GetType().GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                               == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                           (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
