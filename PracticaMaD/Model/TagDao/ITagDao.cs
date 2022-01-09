using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, Int64>
    {
        /// <exception cref="InstanceNotFoundException"/>
        Tag FindByName(string name);

        List<Tag> FindAll();

        List<Tag> FindMostUsedTags(int startIndex, int count);

        List<ImageUpload> FingImagesByTagId(long tagId, int startIndex, int count);

        int CountImagesWithTag(long tagId);
        void UpdateTags(long imgId, List<String> tags);

        /// <exception cref="InstanceNotFoundException"/>
        Tag CreateTag(string name);

    }
}
