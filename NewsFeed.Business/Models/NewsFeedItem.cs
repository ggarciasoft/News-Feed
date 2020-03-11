using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeed.Business.Models
{
    public class NewsFeedItem
    {
        public string Title { get; set; }
        public string ItemUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Creator { get; set; }
        public string Summary { get; set; }
        public string Date { get; set; }
    }
}
