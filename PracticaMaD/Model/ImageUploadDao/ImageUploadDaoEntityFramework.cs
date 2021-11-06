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
        public ImageUploadDaoEntityFramework()
        {
        }

        public List<ImageUpload> FindByTitleOrDescriptionOrCategory(string keyword,int startIndex, int count)
        {
            DbSet<ImageUpload> images = Context.Set<ImageUpload>();

            var result =
                (from a in images
                 where (a.title.Contains(keyword) || a.descriptions.Contains(keyword) || a.category.Contains(keyword))
                 orderby a.uploadDate
                 select a).Skip(startIndex).Take(count).ToList();

            return result;
        }
    }
}
