using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService : ICommentService
    {

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        #region ICommentService Members

        /// <exception cref="InstanceNotFoundException"/>
        void RemoveComment(long commentId)
        {
            Comment comment = CommentDao.Find(commentId);

            CommentDao.Remove(comment);
        }
        #endregion ICommentService Members

    }