using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Ninject;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.PublicationDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService : ICommentService
    {
        #region ICommentService Members
        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public IPublicationDao PublicationDao { private get; set; }

        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }


        /// <exception cref="InstanceNotFoundException"/>
        /// 
        public void AddComment(long pubId, String comment, long userId)
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

            Comment newComment = new Comment();

                newComment.content = comment;
                newComment.usrId = userId;
                newComment.pubId = pubId;
                newComment.comDate = DateTime.Now;

                CommentDao.Create(newComment);
            

        }

        public List<Comment> ShowComments(long pubId, int startIndex, int count)
        {
            Publication pub = PublicationDao.Find(pubId);

            if (pub.Equals(null))
            {
                throw new InstanceNotFoundException(pubId, typeof(long).FullName);
            }

            return CommentDao.FindByPubIdOrderByDateAsc((int)pubId, startIndex ,count+1);
        }

        public void UpdateComment(long commentId, String content)
        {
            Comment comment = CommentDao.Find(commentId);

            if (comment.Equals(null))
            {
                throw new InstanceNotFoundException(commentId, typeof(long).FullName);
            }

            comment.content = content;

            CommentDao.Update(comment);
        }

        

        /// <exception cref="InstanceNotFoundException"/>
        public void RemoveComment(long commentId)
        {
            Comment comment = CommentDao.Find(commentId);

            if (comment.Equals(null))
            {
                throw new InstanceNotFoundException(commentId, typeof(long).FullName);
            }

            CommentDao.Remove(commentId);
        }


        #endregion ICommentService Members

    }
}