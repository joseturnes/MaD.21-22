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
            Tag tag = new Tag();
            try
            {
                string trimmed = String.Concat(name.Where(c => !Char.IsWhiteSpace(c)));
                tag = TagDao.FindByName(trimmed.ToLower());
                if (tag != null)
                {
                    tag.timesUsed++;
                    return tag.tagId;
                }
                
            }
            catch (InstanceNotFoundException)
            {
                string trimmed = String.Concat(name.Where(c => !Char.IsWhiteSpace(c)));
                tag.tagname = trimmed.ToLower();
                TagDao.Create(tag);
                tag.timesUsed++;
                return tag.tagId;
                
            }
            return tag.tagId;            
        }

        [Transactional]
        public List<Tag> findMostUsedTags(int startIndex, int count)
        {
            List<Tag> tags = TagDao.findMostUsedTags(startIndex, count);
            tags.Reverse();
            return tags;
        }

        [Transactional]
        public List<Tag> GetAllTags()
        {
            return TagDao.FindAll();
        }

        [Transactional]
        public int countTags()
        {
            return TagDao.GetAllElements().Count;
        }

        public int countTags(int startIndex, int count)
        {
            throw new NotImplementedException();
        }

        public List<ImageUpload> fingImagesByTagId(long tagId, int startIndex, int count)
        {
            return TagDao.fingImagesByTagId(tagId,startIndex,count);
        }

        public int countImagesWithTag(long tagId)
        {
            return TagDao.countImagesWithTag(tagId);
        }
    }
}
