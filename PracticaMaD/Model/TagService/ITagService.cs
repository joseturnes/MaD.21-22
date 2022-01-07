using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.TagService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    public interface ITagService
    {

        [Inject]
        ITagDao TagDao { set; }

        [Inject]
        IImageUploadDao ImageDao  { set; }

        List<Tag> GetAllTags();

        List<Tag> FindMostUsedTags(int startIndex, int count);

        int CountTags();

        List<ImageUpload> FingImagesByTagId(long tagId, int startIndex, int count);

        int CountImagesWithTag(long tagId);

        void UpdateTags(long imgId, List<String> tags);



    }
}
