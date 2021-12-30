using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Tag, Int64>, ITagDao
    {
        public TagDaoEntityFramework()
        {
        }

        public int countImagesWithTag(long tagId)
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            List<ImageUpload> result =
                (from a in tags
                 where a.tagId == tagId
                 select a.ImageUpload).FirstOrDefault().ToList();

            return result.Count();
        }

        public List<Tag> FindAll()
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            var result = tags.ToList<Tag>();

            return result;
        }

        public Tag FindByName(string name)
        {
            Tag tag = null;

            DbSet<Tag> tags = Context.Set<Tag>();

            string trimmed = String.Concat(name.Where(c => !Char.IsWhiteSpace(c)));

            var result =
                (from u in tags
                 where u.tagname == trimmed
                 select u);

            tag = result.FirstOrDefault();

            if (tag == null)
                throw new InstanceNotFoundException(name,
                    typeof(Tag).FullName);

            return tag;
        }

        public List<Tag> findMostUsedTags(int startIndex, int count)
        {

            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from a in tags
                 orderby a.timesUsed
                 select a).Skip(0).Take(6).ToList();

            return result;
        }

        public List<ImageUpload> fingImagesByTagId(long tagId, int startIndex, int count)
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            List<ImageUpload> result =
                (from a in tags
                 where a.tagId == tagId
                 select a.ImageUpload).FirstOrDefault().Skip(startIndex).Take(count).ToList();

            return result;
        }
    }
}
