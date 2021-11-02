using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;



namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<Comment> FindByPubIdOrderByDateAsc(int pubId, int startIndex, int count)
        {
            throw new NotImplementedException();
        }
    }
}
