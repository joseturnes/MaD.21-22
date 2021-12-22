using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserProfileConversor
    {
        public static List<UserProfileDto> toUserProfilesDtos(List<UserProfile> users)
        {
            List<UserProfileDto> result = new List<UserProfileDto>();

            for (int i = 0; i < users.Count; i++)
            {
                result.Add(new UserProfileDto(users[i].firstName, users[i].lastName, users[i].email, users[i].language, users[i].country));
            }

            return result;
        }
    }
}
