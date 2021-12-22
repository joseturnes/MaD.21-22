using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System;
using Ninject;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public interface ICommentService
    {
        [Inject]
        ICommentDao CommentDao { set; }

        /// <summary>
        /// Add a new comment in a publication.
        /// </summary>
        /// <param name="pubId"> The publication id. </param>
        /// <param name="comment"> The comment. </param>
        long AddComment(long imgId, string comment, long userId);

        /// <summary>
        /// Update a comment.
        /// </summary>
        /// <param name="commentId"> The comment id. </param>
        /// <param name="commentDetails"> The comment details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdateComment(long commentId, String content );

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
        List<CommentDto> ShowComments(long imgId, int startindex, int count);

        /// <exception cref="InstanceNotFoundException"/>
        long CountComents(long imgId);

    }
}
