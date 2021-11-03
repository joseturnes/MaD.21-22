using Es.Udc.DotNet.PracticaMaD.Model.PublicationDao;
using Ninject;

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

        public void LikedPublication(long imgId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePublication(long imgId, PublicationDetails publicationDetails)
        {
            throw new System.NotImplementedException();
        }

        public void UploadPublication()
        {
            throw new System.NotImplementedException();
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