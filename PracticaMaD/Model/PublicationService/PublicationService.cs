using Es.Udc.DotNet.PracticaMaD.Model.PublicationDao;
using Ninject;
using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.PublicationService
{
    public class PublicationService : IPublicationService
    {

        [Inject]
        public IPublicationDao PublicationDao { private get; set; }

        public PublicationDetails FindPublication(string keyword, string category)
        {
            throw new System.NotImplementedException();
        }

        public void LikedPublication(long pubId)
        {
            Publication pub = PublicationDao.Find(pubId);

            if (pub.Equals(null))
            {
                throw new InstanceNotFoundException(pubId, typeof(long).FullName);
            }

            pub.likes++;
            PublicationDao.Update(pub);
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

        public void UploadPublication(long userId, long imgId)
        {
            Publication pub = new Publication();
            
            pub.imgId = imgId;
            pub.usrId = userId;
            pub.likes = 0;
            pub.pubDate = DateTime.Now;

            PublicationDao.Create(pub);

        }

        #region IPublicationService Members

        /// <exception cref="InstanceNotFoundException"/>
        public void RemovePublication(long imgId)
        {
            PublicationDao.Remove(imgId);
        }

        #endregion IPublicationService Members
    }
}