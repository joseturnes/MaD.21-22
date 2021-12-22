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

        /// <summary>
        /// Finds a list of publications of a user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>A list of publications</returns>
        List<ImageUpload> FindByUserIdOrderByDateAsc(int userId, int startIndex, int count);

        List<ImageUpload> FindLastPublications(long userId, int startIndex, int count);

        long CountComments(long imgId, int startIndex, int count);

        List<Comment> FindLastComments(long imgId, int startIndex, int count);

        List<UserProfile> findUserProfiles(long imgId);

    }
}
