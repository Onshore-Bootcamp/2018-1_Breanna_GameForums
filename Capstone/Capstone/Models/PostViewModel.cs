using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class PostViewModel
    {
        public int ThreadId { get; set; }

        public List<PostPO> Posts { get; set; }
    }
}