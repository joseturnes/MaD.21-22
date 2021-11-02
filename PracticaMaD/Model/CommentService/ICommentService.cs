using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface IComment
    {
        [Inject]
        ICommentDao CommentDao { set; }

        /// <summary>
        /// Add a new comment in a publication.
        /// </summary>
        /// <param name="pubId"> The publication id. </param>
        /// <param name="comment"> The comment. </param>
        void AddComment(long pubId, string comment);

        /// <summary>
        /// Update a comment.
        /// </summary>
        /// <param name="commentId"> The comment id. </param>
        /// <param name="commentDetails"> The comment details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdateComment(long commentId, CommentDetails commentDetails );

        /// <summary>
        /// Remove a comment.
        /// </summary>
        /// <param name="commentId"> The comment id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void RemoveComment(long commentId);

        /// <summary>
        /// List of comments.
        /// </summary>
        /// <param name="pubId"> The publication id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        List ShowComments(long pubId);

    }
}
