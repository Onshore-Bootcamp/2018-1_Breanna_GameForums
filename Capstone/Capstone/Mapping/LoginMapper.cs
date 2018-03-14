using Capstone.Models;
using CapstoneDAL.Models;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Capstone.Mapping
{
    public class LoginMapper
    {
        public static List<LoginDO> ListPOToDO(List<LoginPO> dataObject)
        {
            List<LoginDO> mappedItems = new List<LoginDO>();
            foreach (LoginPO item in dataObject)
            {
                mappedItems.Add(LoginPOToDO(item));
            }
            return mappedItems;
        }

        public static LoginDO LoginPOToDO(LoginPO from)
        {
            LoginDO to = new LoginDO();
            to.Username = from.Username;
            to.Password = from.Password;
            return to;
        }

        public static List<LoginPO> ListDOToPO(List<LoginDO> dataObject)
        {
            List<LoginPO> mappedItems = new List<LoginPO>();
            foreach (LoginDO item in dataObject)
            {
                mappedItems.Add(LoginDOToPO(item));
            }
            return mappedItems;
        }

        public static LoginPO LoginDOToPO(LoginDO from)
        {
            LoginPO to = new LoginPO();
            to.Username = from.Username;
            to.Password = from.Password;
            return to;
        }
    }
}