using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
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

        [Inject]
        public IImageUploadDao ImageDao { private get; set; }
        


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

        [Transactional]
        public List<ImageUpload> fingImagesByTagId(long tagId, int startIndex, int count)
        {
            List<ImageUpload> result = TagDao.fingImagesByTagId(tagId, startIndex, count);
            result.Reverse();

            return result;
        }

        [Transactional]
        public int countImagesWithTag(long tagId)
        {
            return TagDao.countImagesWithTag(tagId);
        }
        [Transactional]
        public void updateTags(long imgId, List<string> strtags)
        {
            TagDao.updateTags(imgId, strtags);   
        }
    }
}
