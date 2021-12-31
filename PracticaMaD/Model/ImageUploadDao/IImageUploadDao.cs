using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService;
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

        List<UserProfile> findUserProfiles(long imgId, int startIndex, int count);

        int getNumberOfImages(long userId);

        ImageUpload findImage(long imgId);

        List<ImageUpload> findRecentUploads(int startIndex, int count);

        int countRecentUploads();

        List<Tag> FindImageTags(long imgId, int startIndex, int count);

        int CountImageTags(long imgId);


    }
}
