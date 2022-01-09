using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService
{
    public interface IImageUploadService
    {
        [Inject]
        IImageUploadDao ImageUploadDao { set; }
        [Inject]
        ITagDao TagDao { set; }
        [Inject]
        ICategoryDao CategoryDao { set; }
        [Inject]
        IUserProfileDao UserProfileDao { set; }
        [Inject]
        ITagService TagService { set; }

        /// <summary>
        /// Upload an image.
        /// </summary>
        ///<param name="ImageUploadDetails"> The image params. </param>
        ////// <exception cref="InstanceNotFoundException"/>
        long UploadImage(ImageUploadDetails img, List<string> tags, string category);


        /// <summary>
        /// Update a publication.
        /// </summary>
        /// <param name="imgId"> The publication id. </param>
        /// <param name="ImageUploadDetails"> The publication details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdateImage(long pubId, ImageUploadDetails imageDetails);

        /// <summary>
        /// Remove a image.
        /// </summary>
        /// <param name="pubId"> The publication id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void RemoveImage(long imgId);

        /// <summary>
        /// Feedback with the publication.
        /// </summary>
        /// <param name="pubId"> The publication id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void LikeImage(long imgId, long usrId);

        /// <exception cref="InstanceNotFoundException"/>
        void UnlikeImage(long imgId, long usrId);

        List<ImageUploadDto> RecentUploads(long userId, int startIndex, int count);

        /// <exception cref="InstanceNotFoundException"/>
        ImageUploadDto FindImage(long imgId);

        List<CommentDto> SearchComments(long imgId, int startIndex, int count);

        int CountComments(long imgId);

        int GetNumberOfImages(long userId);

        List<ImageUploadDto> FindByKeywordAndCategory(string keywords, long categoryId, int startIndex, int count);

        /// <exception cref="InstanceNotFoundException"/>
        bool IsLiked(long imgId, long userId);

        int CountSearchKeywords(string keywords, long categoryId);

        List<ImageUploadDto> FindRecentUploads();

        int CountRecentUploads();

        /// <exception cref="InstanceNotFoundException"/>
        List<Tag> FindImageTags(long imgId, int startIndex, int count);

        int CountImageTags(long imgId);

        /// <exception cref="InstanceNotFoundException"/>
        void AddTag(Tag tag, long imgId);

    }
}
