using NewsFeed.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeed.Data.Migrations
{
    public class NewsFeedInitializer : CreateDatabaseIfNotExists<NewsFeedContext>
    {
        protected override void Seed(NewsFeedContext context)
        {
            context.Set<Category>().AddOrUpdate(new Category()
            {
                ID = 1,
                Description = "The Washington Post"
            }, new Category()
            {
                ID = 2,
                Description = "The New York Times"
            }, new Category()
            {
                ID = 3,
                Description = "Fox News"
            }, new Category()
            {
                ID = 4,
                Description = "ESPN"
            }
               );

            context.Set<Models.NewsFeed>().AddOrUpdate(new Models.NewsFeed()
            {
                ID = 1,
                FeedName = "PowerPost",
                FeedUrl = "http://feeds.washingtonpost.com/rss/rss_powerpost",
                CategoryID = 1
            }, new Models.NewsFeed()
            {
                ID = 2,
                FeedName = "Tom Toles",
                FeedUrl = "https://www.washingtonpost.com/people/tom-toles/?outputType=rss",
                CategoryID = 1
            }, new Models.NewsFeed()
            {
                ID = 3,
                FeedName = "Innovations",
                FeedUrl = "http://feeds.washingtonpost.com/rss/rss_innovations",
                CategoryID = 1
            }, new Models.NewsFeed()
            {
                ID = 4,
                FeedName = "Technology",
                FeedUrl = "https://rss.nytimes.com/services/xml/rss/nyt/Technology.xml",
                CategoryID = 2
            }, new Models.NewsFeed()
            {
                ID = 5,
                FeedName = "Sports",
                FeedUrl = "https://rss.nytimes.com/services/xml/rss/nyt/Sports.xml",
                CategoryID = 2
            }, new Models.NewsFeed()
            {
                ID = 6,
                FeedName = "Business",
                FeedUrl = "https://rss.nytimes.com/services/xml/rss/nyt/Business.xml",
                CategoryID = 2
            }, new Models.NewsFeed()
            {
                ID = 8,
                FeedName = "Travels",
                FeedUrl = "https://feeds.foxnews.com/foxnews/internal/travel/mixed",
                CategoryID = 3
            }, new Models.NewsFeed()
            {
                ID = 9,
                FeedName = "World",
                FeedUrl = "https://feeds.foxnews.com/foxnews/world",
                CategoryID = 3
            }, new Models.NewsFeed()
            {
                ID = 10,
                FeedName = "NBA",
                FeedUrl = "https://www.espn.com/espn/rss/nba/news",
                CategoryID = 4
            }, new Models.NewsFeed()
            {
                ID = 11,
                FeedName = "NFL",
                FeedUrl = "https://www.espn.com/espn/rss/nfl/news",
                CategoryID = 4
            }, new Models.NewsFeed()
            {
                ID = 12,
                FeedName = "MLB",
                FeedUrl = "https://www.espn.com/espn/rss/mlb/news",
                CategoryID = 4
            });

            base.Seed(context);
        }
    }
}
