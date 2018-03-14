using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class ThreadViewModel
    {
        public float AverageWords { get; set; }

        public List<ThreadPO> Threads { get; set; }
    }
}