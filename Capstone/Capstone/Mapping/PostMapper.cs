using Capstone.Models;
using CapstoneBLL.Models;
using CapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Mapping
{
    public class PostMapper
    {
        public static List<PostPO> ListDOToPO(List<PostDO> dataObject)
        {
            List<PostPO> mappedItems = new List<PostPO>();
            foreach (PostDO item in dataObject)
            {
                mappedItems.Add(PostDOToPO(item));
            }
            return mappedItems;
        }

        public static List<PostDO> ListPOToDO(List<PostPO> dataObject)
        {
            List<PostDO> mappedItems = new List<PostDO>();
            foreach (PostPO item in dataObject)
            {
                mappedItems.Add(PostPOToDO(item));
            }
            return mappedItems;
        }

        public static List<PostBO> ListPOToBO(List<PostPO> dataObject)
        {
            List<PostBO> mappedItems = new List<PostBO>();
            foreach (PostPO item in dataObject)
            {
                mappedItems.Add(PostPOToBO(item));
            }
            return mappedItems;
        }

        public static List<PostPO> ListBOToPO(List<PostBO> dataObject)
        {
            List<PostPO> mappedItems = new List<PostPO>();
            foreach (PostBO item in dataObject)
            {
                mappedItems.Add(PostBOToPO(item));
            }
            return mappedItems;
        }

        public static PostPO PostDOToPO(PostDO from)
        {
            PostPO to = new PostPO();
            to.PostId = from.PostId;
            to.UserId = from.UserId;
            to.Username = from.Username;   
            to.ThreadId = from.ThreadId;
            to.CreationDate = from.CreationDate;
            to.EditDate = from.EditDate;
            to.Title = from.Title;
            to.Content = from.Content;
            return to;
        }

        public static PostDO PostPOToDO(PostPO from)
        {
            PostDO to = new PostDO();
            to.PostId = from.PostId;
            to.UserId = from.UserId;
            to.Username = from.Username;
            to.ThreadId = from.ThreadId;
            to.CreationDate = from.CreationDate;
            to.EditDate = from.EditDate;
            to.Title = from.Title;
            to.Content = from.Content;
            return to;
        }

        public static PostBO PostPOToBO(PostPO from)
        {
            PostBO to = new PostBO();
            to.PostId = from.PostId;
            to.UserId = from.UserId;
            to.Username = from.Username;
            to.ThreadId = from.ThreadId;
            to.CreationDate = from.CreationDate;
            to.EditDate = from.EditDate;
            to.Title = from.Title;
            to.Content = from.Content;
            return to;
        }

        public static PostPO PostBOToPO(PostBO from)
        {
            PostPO to = new PostPO();
            to.PostId = from.PostId;
            to.UserId = from.UserId;
            to.Username = from.Username;
            to.ThreadId = from.ThreadId;
            to.CreationDate = from.CreationDate;
            to.EditDate = from.EditDate;
            to.Title = from.Title;
            to.Content = from.Content;
            return to;
        }
    }
}