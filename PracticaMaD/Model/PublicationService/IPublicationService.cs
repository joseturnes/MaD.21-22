using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.PublicationDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.PublicationDao
{
    public interface IPublication
    {
        [Inject]
        IPublicationDao PublicationDao { set; }

        /// <summary>
        /// Upload a image in a publication.
        /// </summary>
        ///<param name="userProfileId"> The user profile id. </param>
        void UploadPublication();

        /// <summary>
        /// Update a publication.
        /// </summary>
        /// <param name="imgId"> The image id. </param>
        /// <param name="publicationDetails"> The publication details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdatePublication(long imgId, PublicationDetails publicationDetails );

        /// <summary>
        /// Remove a publication.
        /// </summary>
        /// <param name="imgId"> The image id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void RemovePublication(long imgId);

        /// <summary>
        /// Remove a publication.
        /// </summary>
        /// <param name="keyword"> The keyword. </param>
        /// <param name="category"> The publcation category. </param>
        /// <exception cref="InstanceNotFoundException"/>
        PublicationDetails FindPublication(string keyword, string category);


        /// <summary>
        /// Feedback with the publication.
        /// </summary>
        /// <param name="imgId"> The image id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void LikedPublication(long imgId);
    }
}
