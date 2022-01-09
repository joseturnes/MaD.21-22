using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Tag, Int64>, ITagDao
    {
        public TagDaoEntityFramework()
        {
        }

        [Inject]
        public IImageUploadDao ImageDao { private get; set; }

        public int CountImagesWithTag(long tagId)
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

        /// <exception cref="InstanceNotFoundException"/>
        public Tag FindByName(string name)
        {
            Tag tag = null;

            DbSet<Tag> tags = Context.Set<Tag>();

            string trimmed = String.Concat(name.Where(c => !Char.IsWhiteSpace(c))).ToLower();

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

        public List<Tag> FindMostUsedTags(int startIndex, int count)
        {

            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from a in tags
                 orderby a.timesUsed descending
                 select a).Skip(0).Take(6).ToList();

            result.Reverse();

            return result;
        }

        public List<ImageUpload> FingImagesByTagId(long tagId, int startIndex, int count)
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            List<ImageUpload> result =
                (from a in tags
                 where a.tagId == tagId
                 select a.ImageUpload).FirstOrDefault().Skip(startIndex).Take(count).ToList();

            return result;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public Tag CreateTag(string name)
        {
            Tag tag = new Tag();
            try
            {
                string trimmed = String.Concat(name.Where(c => !Char.IsWhiteSpace(c)));
                tag = FindByName(trimmed.ToLower());
                if (tag != null)
                {
                    return tag;
                }

            }
            catch (InstanceNotFoundException)
            {
                string trimmed = String.Concat(name.Where(c => !Char.IsWhiteSpace(c)));
                tag.tagname = trimmed.ToLower();
                Create(tag);
                return tag;

            }
            return tag;
        }

        public void UpdateTags(long imgId, List<String> strtags)
        {
            ImageUpload image = ImageDao.Find(imgId);

            image.Tag.Clear();

            strtags.ForEach(t => t.ToLower());

            if (image != null && strtags != null)
            {
                var imageTags = image.Tag;

                foreach (var tag in imageTags)
                {
                    if (!strtags.Contains(tag.tagname))
                        image.Tag.Remove(tag);
                }


                foreach (String tag in strtags)
                {
                    if (!tag.Equals(""))
                    {
                        Tag tagEntity = CreateTag(tag);
                        if (!image.Tag.Contains(tagEntity))
                        {
                            tagEntity.timesUsed++;
                            image.Tag.Add(tagEntity);
                            tagEntity.ImageUpload.Add(image);
                        }
                    }
                }
            }
            ImageDao.Update(image);
        }
    }
}
