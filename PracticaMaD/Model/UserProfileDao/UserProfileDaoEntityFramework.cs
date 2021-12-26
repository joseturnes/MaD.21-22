using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    /// <summary>
    /// Specific Operations for UserProfile
    /// </summary>
    public class UserProfileDaoEntityFramework :
        GenericDaoEntityFramework<UserProfile, Int64>, IUserProfileDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public UserProfileDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IUserProfileDao Members. Specific Operations

        /// <summary>
        /// Finds a UserProfile by his loginName
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;

            #region Option 1: Using Linq.

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.loginName == loginName
                 select u);

            userProfile = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);

            return userProfile;
        }

        /// <summary>
        /// Finds a UserProfile by his id
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        private UserProfile FindById(long id)
        {
            UserProfile userProfile = null;

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.usrId == id
                 select u);

            userProfile = result.FirstOrDefault();

       

            if (userProfile == null)
                throw new InstanceNotFoundException(id,
                    typeof(UserProfile).FullName);

            return userProfile;
        }

        public List<UserProfile> FindFollowers(long userId,int startIndex,
            int count)
        {
            DbSet<UserProfile> followers = Context.Set<UserProfile>();

            var result =
                (from a in followers
                 where a.UserProfile1.Equals(a)
                 select a).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<UserProfile> FindFollows(long userId, int startIndex, int count)
        {
            UserProfile userProfile = null;

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.usrId == userId
                 select u).FirstOrDefault();

            return result.UserProfile2.Skip(startIndex).Take(count).ToList();
        }

        public int getNumberOfFollows(long id)
        {
            UserProfile userProfile = null;

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.usrId == id
                 select u).FirstOrDefault();

            return result.UserProfile2.Count();

            #endregion IUserProfileDao Members
        }
    }
}


