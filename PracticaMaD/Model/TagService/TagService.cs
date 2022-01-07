using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    public class TagService : ITagService
    {
        [Inject]
        public ITagDao TagDao { private get; set; }

        [Inject]
        public IImageUploadDao ImageDao { private get; set; }



        [Transactional]
        public List<Tag> FindMostUsedTags(int startIndex, int count)
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
        public int CountTags()
        {
            return TagDao.GetAllElements().Count;
        }

        [Transactional]
        public List<ImageUpload> FingImagesByTagId(long tagId, int startIndex, int count)
        {
            List<ImageUpload> result = TagDao.fingImagesByTagId(tagId, startIndex, count);
            result.Reverse();

            return result;
        }

        [Transactional]
        public int CountImagesWithTag(long tagId)
        {
            return TagDao.countImagesWithTag(tagId);
        }
        [Transactional]
        public void UpdateTags(long imgId, List<string> strtags)
        {
            TagDao.updateTags(imgId, strtags);
        }
    }
}
