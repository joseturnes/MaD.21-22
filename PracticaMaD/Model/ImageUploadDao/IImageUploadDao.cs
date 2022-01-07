using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao
{
    public interface IImageUploadDao : IGenericDao<ImageUpload, Int64>
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }
        List<ImageUpload> FindByTitleOrDescription(string keyword, int startIndex, int count);

        List<ImageUpload> FindByCategory(long categoryId, int startIndex, int count);

        /// <summary>
        /// Finds a list of publications of a user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>A list of publications</returns>
        List<ImageUpload> FindByUserIdOrderByDateAsc(int userId, int startIndex, int count);

        List<ImageUpload> FindLastPublications(long userId, int startIndex, int count);

        int CountComments(long imgId);

        List<Comment> FindLastComments(long imgId, int startIndex, int count);

        List<UserProfile> FindUserProfiles(long imgId, int startIndex, int count);

        int GetNumberOfImages(long userId);

        ImageUpload FindImage(long imgId);

        List<ImageUpload> FindRecentUploads();

        int CountRecentUploads();

        List<Tag> FindImageTags(long imgId, int startIndex, int count);

        int CountImageTags(long imgId);

        bool IsLiked(long imgId, long usrId);

        void LikeImage(long imgId, long usrId);

        void UnlikeImage(long imgId, long usrId);

    }
}
