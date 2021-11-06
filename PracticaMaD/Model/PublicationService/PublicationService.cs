using Es.Udc.DotNet.PracticaMaD.Model.PublicationDao;
using Ninject;
using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.PublicationService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.PublicationService
{
    public class PublicationService : IPublicationService
    {

        [Inject]
        public IPublicationDao PublicationDao { private get; set; }
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }
        [Inject]
        public IImageUploadDao ImageUploadDao { private get; set; }

        #region IPublicationService Members

        public PublicationDetails FindPublication(string keyword, string category)
        {
            throw new System.NotImplementedException();
        }

        public void LikedPublication(long pubId, long userId)
        {
            Publication pub = PublicationDao.Find(pubId);
            UserProfile user = UserProfileDao.Find(userId);

            if (pub.Equals(null))
            {
                throw new InstanceNotFoundException(pubId, typeof(long).FullName);
            }

            if (user.Equals(null))
            {
                throw new InstanceNotFoundException(userId, typeof(long).FullName);
            }

            if (pub.UserProfile1.Contains(user) || user.Publication1.Contains(pub))
            {
                pub.likes--;
                pub.UserProfile1.Remove(user);
                PublicationDao.Update(pub);
                user.Publication1.Remove(pub);
                UserProfileDao.Update(user);
            }
            else
            {
                pub.likes++;
                pub.UserProfile1.Add(user);
                PublicationDao.Update(pub);
                user.Publication1.Add(pub);
                UserProfileDao.Update(user);
            }
        }

        public void UpdatePublication(long pubId, PublicationDetails publicationDetails)
        {
            Publication pub = PublicationDao.Find(pubId);

            if (pub.Equals(null))
            {
                throw new InstanceNotFoundException(pubId, typeof(long).FullName);
            }

            pub.imgId = (long)publicationDetails.imgId;
            pub.usrId = publicationDetails.userId;
            pub.likes = publicationDetails.likes;
            pub.pubDate = publicationDetails.pubDate;

            PublicationDao.Update(pub);
        }

        public long UploadPublication(long userId, long imgId)
        {
            Publication pub = new Publication();
            
            pub.imgId = imgId;
            pub.usrId = userId;
            pub.likes = 0;
            pub.pubDate = DateTime.Now;

            PublicationDao.Create(pub);

            return pub.pubId;

        }

        
        /// <exception cref="InstanceNotFoundException"/>
        public void RemovePublication(long pubId)
        {
            Publication pub = PublicationDao.Find(pubId);
            long imgId = pub.imgId;
            PublicationDao.Remove(pubId);
            ImageUploadDao.Remove(imgId);
        }

        #endregion IPublicationService Members
    }
}