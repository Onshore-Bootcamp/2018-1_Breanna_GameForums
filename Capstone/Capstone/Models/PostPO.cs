﻿using System;

namespace Capstone.Models
{
    public class PostPO
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public int ThreadId { get; set; }
        
        public DateTime CreationDate { get; set; }

        public DateTime EditDate { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }
    }
}