using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, Int64>
    {
        Tag FindByName(string name);

        List<Tag> FindAll();

        List<Tag> findMostUsedTags(int startIndex, int count);

        List<ImageUpload> fingImagesByTagId(long tagId, int startIndex, int count);

        int countImagesWithTag(long tagId);
        void updateTags(long imgId, List<String> tags);

        Tag CreateTag(string name);

    }
}
