using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {
        /// <summary>
        /// Finds a list of comments that reference a publication
        /// </summary>
        /// <param name="pubId">pubId</param>
        /// <returns>A list of comments</returns>
        List<Comment> FindByPubIdOrderByDateAsc(int pubId, int startIndex, int count);

    }
}
