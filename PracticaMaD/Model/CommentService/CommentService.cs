using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Ninject;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService : ICommentService
    {
        #region ICommentService Members
        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public IImageUploadDao ImageUploadDao { private get; set; }

        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }


        /// <exception cref="InstanceNotFoundException"/>
        /// 
        [Transactional]
        public long AddComment(long imgId, String comment, long userId)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);
            UserProfile user = UserProfileDao.Find(userId);

            if (img.Equals(null))
            {
                throw new InstanceNotFoundException(imgId, typeof(long).FullName);
            }

            if (user.Equals(null))
            {
                throw new InstanceNotFoundException(userId, typeof(long).FullName);
            }

            Comment newComment = new Comment();

                newComment.content = comment;
                newComment.usrId = userId;
                newComment.imgId = imgId;
                newComment.comDate = DateTime.Now;

                CommentDao.Create(newComment);
            
            return newComment.commentId;

        }

        [Transactional]
        public List<CommentDto> ShowComments(long imgId, int startIndex, int count)
        {
            ImageUpload pub = ImageUploadDao.Find(imgId);

            if (pub.Equals(null))
            {
                throw new InstanceNotFoundException(imgId, typeof(long).FullName);
            }

            List <Comment> coments = CommentDao.FindByPubIdOrderByDateAsc((int)imgId, startIndex, count + 1);
            List<CommentDto> result = new List<CommentDto>();
            coments.ToArray();

            result = CommentConversor.toCommentDtos(coments);

            return result;
        }

        [Transactional]
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


        [Transactional]
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

        [Transactional]
        /// <exception cref="InstanceNotFoundException"/>
        public long CountComents(long imgId)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);

            if (img.Equals(null))
            {
                throw new InstanceNotFoundException(img, typeof(ImageUpload).FullName);
            }

            return ImageUploadDao.CountComments(imgId);
        }


        #endregion ICommentService Members

    }
}