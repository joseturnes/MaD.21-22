using Es.Udc.DotNet.PracticaMaD.Model.PublicationDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class PublicationService : IPublicationService {

    [Inject]
    public IPublicationDao PublicationDao { private get; set; }

        #region IPublicationService Members

        /// <exception cref="InstanceNotFoundException"/>
        void RemovePublication(long imgId)
        {
            Publication publication = PublicationDao.Find(imgId);

            PublicationDao.Remove(publication);
        }

        #endregion IPublicationService Members
    }