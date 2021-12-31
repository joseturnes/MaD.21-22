using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, Int64>
    {
        Tag FindByName(string name);

        List<Tag> FindAll();

        List<Tag> findMostUsedTags(int startIndex,int count);

        List<ImageUpload> fingImagesByTagId(long tagId, int startIndex, int count);

        int countImagesWithTag(long tagId);

    }
}
