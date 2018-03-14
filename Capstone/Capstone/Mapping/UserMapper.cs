using Capstone.Models;
using System.Collections.Generic;
using CapstoneDAL.Models;


namespace Capstone.Mapping
{
    public class UserMapper
    {
        public static List<UserPO> ListDOToPO(List<UserDO> dataObject)
        {
            List<UserPO> mappedItems = new List<UserPO>();
            foreach (UserDO item in dataObject)
            {
                mappedItems.Add(UserDOToPO(item));
            }
            return mappedItems;
        }

        public static UserDO UserPOToDO(UserPO from)
        {
            UserDO to = new UserDO();
            to.UserId = from.UserId;
            to.RoleId = from.RoleId;
            to.Username = from.Username;
            to.Password = from.Password;
            to.Email = from.Email;
            to.Name = from.Name;
            return to;
        }

        public static List<UserDO> ListPOToDO(List<UserPO> dataObject)
        {
            List<UserDO> mappedItems = new List<UserDO>();
            foreach (UserPO item in dataObject)
            {
                mappedItems.Add(UserPOToDO(item));
            }
            return mappedItems;
        }

        public static UserPO UserDOToPO(UserDO from)
        {
            UserPO to = new UserPO();
            to.UserId = from.UserId;
            to.RoleId = from.RoleId;
            to.Username = from.Username;
            to.Password = from.Password;
            to.Email = from.Email;
            to.Name = from.Name;
            return to;
        }
    }
}