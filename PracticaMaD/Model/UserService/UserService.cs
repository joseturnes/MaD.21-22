using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        #region IUserService Members

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword =
            PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.loginName,userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.language, userProfile.country);

            return userProfileDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted)
        {
            UserProfile userProfile =
                UserProfileDao.FindByLoginName(loginName);

            String storedPassword = userProfile.enPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(userProfile.usrId, userProfile.firstName,
                storedPassword, userProfile.language, userProfile.country);
        }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(string loginName, string clearPassword,
            UserProfileDetails userProfileDetails)
        {
            try
            {
                UserProfileDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserProfile).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile = new UserProfile();

                userProfile.loginName = loginName;
                userProfile.enPassword = encryptedPassword;
                userProfile.firstName = userProfileDetails.FirstName;
                userProfile.lastName = userProfileDetails.Lastname;
                userProfile.email = userProfileDetails.Email;
                userProfile.language = userProfileDetails.Language;
                userProfile.country = userProfileDetails.Country;

                UserProfileDao.Create(userProfile);

                return userProfile.usrId;
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails)
        {
            UserProfile userProfile =
                UserProfileDao.Find(userProfileId);

            userProfile.firstName = userProfileDetails.FirstName;
            userProfile.lastName = userProfileDetails.Lastname;
            userProfile.email = userProfileDetails.Email;
            userProfile.language = userProfileDetails.Language;
            userProfile.country = userProfileDetails.Country;
            UserProfileDao.Update(userProfile);
        }


        public bool UserExists(string loginName)
        {

            try
            {
                UserProfile userProfile = UserProfileDao.FindByLoginName(loginName);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }

            return true;
        }

       
        public void follow(string followedLogin, string followerLogin)
        {
            UserProfile followed = UserProfileDao.FindByLoginName(followedLogin);
            UserProfile follower = UserProfileDao.FindByLoginName(followerLogin);

            if (followed.Equals(null))
            {
                throw new InstanceNotFoundException(followedLogin, typeof(UserProfile).FullName);
            }
            if (follower.Equals(null))
            {
                throw new InstanceNotFoundException(followerLogin, typeof(UserProfile).FullName);
            }

            if (!(followed.UserProfile2.Contains(follower) && follower.UserProfile1.Contains(followed)))
            {
                followed.UserProfile2.Add(follower);
                follower.UserProfile1.Add(followed);
            }

        }

        public List<UserProfileDto> FollowerList(long userId, int startIndex, int count)
        {
            List<UserProfile> users = UserProfileDao.FindFollowers(userId, startIndex, count);
            return UserProfileConversor.toUserProfilesDtos(users);
        }

        public List<UserProfileDto> ListOfFollows(long userId, int startIndex, int count)
        {
            List<UserProfile> users = new List<UserProfile>();
            users = UserProfileDao.FindFollows(userId, startIndex, count);
          
            
            return UserProfileConversor.toUserProfilesDtos(users);
            
        }

       public int getNumberOfFollows(long userId) 
        {

            return UserProfileDao.getNumberOfFollows(userId);

        }

        public int getNumberOfFollowers(long userId)
        {
            return UserProfileDao.getNumberOfFollowers(userId);
        }

        public bool isFollowed(long userId1, long userId2)
        {
            int numberFollows = getNumberOfFollows(userId2);
            List<UserProfileDto> result = ListOfFollows(userId2, 0, numberFollows);
            UserProfileDetails perfil = FindUserProfileDetails(userId1);
            UserProfileDto perfilDto = new UserProfileDto(userId1, perfil.userName,
                perfil.FirstName, perfil.Lastname, perfil.Email, perfil.Language, perfil.Country);
            if (result.Contains(perfilDto))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string findUserNameById(long userId)
        {
            return FindUserProfileDetails(userId).userName;
        }

        
    }
        #endregion IUserService Members
}