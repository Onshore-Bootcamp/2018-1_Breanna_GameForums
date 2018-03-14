using CapstoneBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneBLL
{
    public class ThreadBLL
    {
        public float AverageWordsPerPost(List <PostBO> listBO)
        {
            int wordCount = 0;
            
            foreach(PostBO post in listBO)
            {
                String[] words = post.Content.Split(' ');
                wordCount += words.Length;
            }
            float averageWordsPerPost = wordCount / (float)listBO.Count;
            return averageWordsPerPost;
        }
    }
}
