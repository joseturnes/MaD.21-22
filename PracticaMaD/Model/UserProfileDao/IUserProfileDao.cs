using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public interface IUserProfileDao : IGenericDao<UserProfile, Int64>
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile FindByLoginName(String loginName);

        /// <summary>
        /// Finds a List of followers
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>The followers list or an empty list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<UserProfile> FindFollowers(long userId, int startIndex, int count);


        List<UserProfile> FindFollows(long userId, int startIndex, int count);

        int getNumberOfFollows(long userId);



    }


}
