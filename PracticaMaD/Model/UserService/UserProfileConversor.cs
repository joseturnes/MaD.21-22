﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserProfileConversor
    {
        public static UserProfileDto ToUserProfileDto(UserProfile user)
        {
            return new UserProfileDto(user.usrId,user.loginName, user.firstName, user.lastName, user.email, user.language, user.country);
        }
        public static List<UserProfileDto> ToUserProfilesDtos(List<UserProfile> users)
        {
            List<UserProfileDto> result = new List<UserProfileDto>();

            for (int i = 0; i < users.Count; i++)
            {
                result.Add(ToUserProfileDto(users[i]));
            }

            return result;
        }

        
    }
}
