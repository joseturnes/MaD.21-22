using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao
{
    public interface IImageUploadDao : IGenericDao<ImageUpload, Int64>
    {
        List<ImageUpload> FindByTitleOrDescriptionOrCategory(string keyword, int startIndex, int count);

    }
}
