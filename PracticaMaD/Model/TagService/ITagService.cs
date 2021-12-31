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




        /// <exception cref="AlreadyCreatedException"/>
        long CreateTag(string name);

        List<Tag> GetAllTags();

        List<Tag> findMostUsedTags(int startIndex, int count);

        int countTags();

        List<ImageUpload> fingImagesByTagId(long tagId, int startIndex, int count);

        int countImagesWithTag(long tagId);

        void updateTags(long imgId, List<String> tags);



    }
}
