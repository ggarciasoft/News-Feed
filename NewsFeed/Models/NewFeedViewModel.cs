using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsFeed.Models
{
    public class NewFeedViewModel
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string NewsFeedName { get; set; }
        [Required]
        [Url]
        public string NewsFeedURL { get; set; }
    }
}