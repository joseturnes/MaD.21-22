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

        public CommentDaoEntityFramework()
        {
        }
        public List<Comment> FindByPubIdOrderByDateAsc(int pubId, int startIndex, int count)
        {
            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from a in comments
                 where a.imgId == pubId
                 orderby a.comDate
                 select a).Skip(startIndex).Take(count).ToList();

            return result;
        }
    }
}
