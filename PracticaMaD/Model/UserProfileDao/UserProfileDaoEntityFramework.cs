using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
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
        public UserProfile FindById(long id)
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

        public List<UserProfile> FindFollowers(long userId, int startIndex,
            int count)
        {
            UserProfile userProfile = FindById(userId);


            return userProfile.UserProfile2.Skip(startIndex).Take(count).ToList();
        }

        public List<UserProfile> FindFollows(long userId, int startIndex, int count)
        {
            UserProfile userProfile = FindById(userId);


            return userProfile.UserProfile1.Skip(startIndex).Take(count).ToList();
        }

        public int GetNumberOfFollows(long userId)
        {
            UserProfile userProfile = FindById(userId);


            return userProfile.UserProfile1.Count();
        }

        public int GetNumberOfFollowers(long userId)
        {
            UserProfile userProfile = FindById(userId);


            return userProfile.UserProfile2.Count();


        }

        public void Follow(UserProfile followed, UserProfile follower)
        {
            if (!(followed.UserProfile2.Contains(follower) && follower.UserProfile1.Contains(followed)))
            {
                followed.UserProfile2.Add(follower);
                follower.UserProfile1.Add(followed);
            }
        }
        #endregion IUserProfileDao Members
    }
}


