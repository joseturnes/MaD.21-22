using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.PublicationDao
{
    public interface IPublicationDao : IGenericDao<Publication, Int64>
    {
        /// <summary>
        /// Finds a list of publications of a user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>A list of publications</returns>
        List<Publication> FindByUserIdOrderByDateAsc(int userId, int startIndex, int count);
    }
}
