using Capstone.Models;
using CapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Mapping
{
    public class RegisterMapper
    {
        public static List<RegisterDO> ListPOToDO(List<RegisterPO> dataObject)
        {
            List<RegisterDO> mappedItems = new List<RegisterDO>();
            foreach (RegisterPO item in dataObject)
            {
                mappedItems.Add(RegisterPOToDO(item));
            }
            return mappedItems;
        }

        public static RegisterDO RegisterPOToDO(RegisterPO from)
        {
            RegisterDO to = new RegisterDO();
            to.Username = from.Username;
            to.Password = from.Password;
            to.Email = from.Email;
            to.Name = from.Name;
            return to;
        }

        public static List<RegisterPO> ListDOToPO(List<RegisterDO> dataObject)
        {
            List<RegisterPO> mappedItems = new List<RegisterPO>();
            foreach (RegisterDO item in dataObject)
            {
                mappedItems.Add(RegisterDOToPO(item));
            }
            return mappedItems;
        }

        public static RegisterPO RegisterDOToPO(RegisterDO from)
        {
            RegisterPO to = new RegisterPO();
            to.Username = from.Username;
            to.Password = from.Password;
            to.Email = from.Email;
            to.Name = from.Name;
            return to;
        }
    }
}