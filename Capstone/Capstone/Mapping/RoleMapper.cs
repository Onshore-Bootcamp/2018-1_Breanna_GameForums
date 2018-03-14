using Capstone.Models;
using CapstoneDAL.Models;
using System.Collections.Generic;

namespace Capstone.Mapping
{
    public class RoleMapper
    {
        public static List<RolePO> ListDOToPO(List<RoleDO> dataObject)
        {
            List<RolePO> mappedItems = new List<RolePO>();
            foreach (RoleDO item in dataObject)
            {
                mappedItems.Add(RoleDOToPO(item));
            }
            return mappedItems;
        }

        public static RoleDO RolePOToDO(RolePO from)
        {
            RoleDO to = new RoleDO();
            to.RoleId = from.RoleId;
            to.Name = from.Name;
            return to;
        }

        public static List<RoleDO> ListPOToDO(List<RolePO> dataObject)
        {
            List<RoleDO> mappedItems = new List<RoleDO>();
            foreach (RolePO item in dataObject)
            {
                mappedItems.Add(RolePOToDO(item));
            }
            return mappedItems;
        }

        public static RolePO RoleDOToPO(RoleDO from)
        {
            RolePO to = new RolePO();
            to.RoleId = from.RoleId;
            to.Name = from.Name;
            return to;
        }
    }
}