using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserProfileConversor
    {
        public static UserProfileDto toUserProfileDto(UserProfile user)
        {
            return new UserProfileDto(user.loginName, user.firstName, user.lastName, user.email, user.language, user.country);
        }
        public static List<UserProfileDto> toUserProfilesDtos(List<UserProfile> users)
        {
            List<UserProfileDto> result = new List<UserProfileDto>();

            for (int i = 0; i < users.Count; i++)
            {
                result.Add(toUserProfileDto(users[i]));
            }

            return result;
        }

        
    }
}
