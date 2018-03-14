using Capstone.Models;
using CapstoneDAL.Models;
using System.Collections.Generic;

namespace Capstone.Mapping
{
    public class ThreadMapper
    {
        public static List<ThreadPO> ListDOToPO(List<ThreadDO> dataObject)
        {
            List<ThreadPO> mappedItems = new List<ThreadPO>();
            foreach (ThreadDO item in dataObject)
            {
                mappedItems.Add(ThreadDOToPO(item));
            }
            return mappedItems;
        }

        public static ThreadPO ThreadDOToPO(ThreadDO from)
        {
            ThreadPO to = new ThreadPO();
            to.ThreadId = from.ThreadId;
            to.Title = from.Title;
            to.Information = from.Information;
            return to;
        }

        public static List<ThreadDO> ListPOToDO(List<ThreadPO> dataObject)
        {
            List<ThreadDO> mappedItems = new List<ThreadDO>();
            foreach (ThreadPO item in dataObject)
            {
                mappedItems.Add(ThreadPOToDO(item));
            }
            return mappedItems;
        }

        public static ThreadDO ThreadPOToDO(ThreadPO from)
        {
            ThreadDO to = new ThreadDO();
            to.ThreadId = from.ThreadId;
            to.Title = from.Title;
            to.Information = from.Information;
            return to;
        }
    }
}