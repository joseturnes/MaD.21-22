using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService.Exceptions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    public class TagService : ITagService
    {
        [Inject]
        public ITagDao TagDao { private get;set ; }

        [Transactional]
        public long CreateTag(string name)
        {
            long returned;
            Tag tag = new Tag();
            try
            {
                tag = TagDao.FindByName(name);
                if (tag != null)
                {
                    tag.timesUsed++;
                    return tag.tagId;
                }
                
            }
            catch (InstanceNotFoundException)
            {          
                tag.tagname = name;
                TagDao.Create(tag);
                tag.timesUsed++;
                return tag.tagId;
                
            }
            return tag.tagId;            
        }

        [Transactional]
        public List<Tag> GetAllTags()
        {
            return TagDao.FindAll();
        }
    }
}
