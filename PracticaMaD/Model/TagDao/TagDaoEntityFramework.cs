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

            var result =
                (from u in tags
                 where u.tagname == name
                 select u);

            tag = result.FirstOrDefault();

            if (tag == null)
                throw new InstanceNotFoundException(name,
                    typeof(Tag).FullName);

            return tag;
        }

        
    }
}
