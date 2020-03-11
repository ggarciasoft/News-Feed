using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeed.Data.Models
{
    public class NewsFeed
    {
        public int ID { get; set; }
        public string FeedName { get; set; }
        public string FeedUrl { get; set; }
        public int CategoryID { get; set; }
        public bool Subscribed { get; set; }
        public virtual Category Category { get; set; }
    }
}
