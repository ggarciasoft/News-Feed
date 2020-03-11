using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeed.Data.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<NewsFeed> NewsFeeds { get; set; }
    }
}
