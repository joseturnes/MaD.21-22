using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao
{
    public class ImageUploadDaoEntityFramework : 
        GenericDaoEntityFramework<ImageUpload, Int64>, IImageUploadDao
    {
        public ImageUploadDaoEntityFramework()
        {
        }
    }
}
