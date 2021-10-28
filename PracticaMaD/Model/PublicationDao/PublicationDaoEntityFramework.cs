using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.PublicationDao
{
    public class PublicationDaoEntityFramework : 
        GenericDaoEntityFramework<Publication,Int64>, IPublicationDao
    {

        public PublicationDaoEntityFramework()
        {
        }

        public List<Publication> FindByUserIdOrderByDateAsc(int userId, int startIndex,
            int count)
        {
            DbSet<Publication> publications = Context.Set<Publication>();

            var result =
                (from a in publications
                 where a.usrId == userId
                 orderby a.pubDate
                 select a).Skip(startIndex).Take(count).ToList();

            return result;
        }
    }
}
